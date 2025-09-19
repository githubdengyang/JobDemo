using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SG
{
    public class PlayerCombatManager : CharacterCombatManeger
    {
        PlayerManager player;

        public WeaponItem currentWeaponBeingUsed;

        [Header("Flags")]
        public bool canComboWithMainHandWeapon = false;
        //public bool canComboWithOffHandWeapon = false;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            if (player.IsOwner)
            {
                //  PERFORM THE ACTION
                weaponAction.AttemptToPerformAction(player, weaponPerformingAction);

                //  NOTIFY THE SERVER WE HAVE PERFORMED THE ACTION, SO WE PERFORM IT FROM THERE PERSPECTIVE ALSO
                player.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
            }
        }

        public override void CloseAllDamageColliders()
        {
            base.CloseAllDamageColliders();

            player.playerEquipmentManager.rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
            player.playerEquipmentManager.leftWeaponManager.meleeDamageCollider.DisableDamageCollider();
        }

        //  CRITICAL ATTACKS
        public override void AttemptRiposte(RaycastHit hit)
        {
            CharacterManager targetCharacter = hit.transform.gameObject.GetComponent<CharacterManager>();

            //  IF FOR SOME REASON THE TARGET CHARACTER IS NULL, RETURN
            if (targetCharacter == null)
                return;

            //  IF SOME HOW SINCE THE INITIAL CHECK THE CHARACTER CAN NO LONGER BE RIPOSTED, RETURN
            if (!targetCharacter.characterNetworkManager.isRipostable.Value)
                return;

            //  IF SOMEBODY ELSE IS ALREADY PERFORMING A CRITICAL STRIKE ON THE CHARACTER (OR WE ALREADY ARE), RETURN
            if (targetCharacter.characterNetworkManager.isBeingCriticallyDamaged.Value)
                return;

            //  YOU CAN ONLY RIPOSTE WITH A MELEE WEAPON ITEM
            MeleeWeaponItem riposteWeapon;
            MeleeWeaponDamageCollider riposteCollider;

            if (player.playerNetworkManager.isTwoHandingLeftWeapon.Value)
            {
                riposteWeapon = player.playerInventoryManager.currentLeftHandWeapon as MeleeWeaponItem;
                riposteCollider = player.playerEquipmentManager.leftWeaponManager.meleeDamageCollider;
            }
            else
            {
                riposteWeapon = player.playerInventoryManager.currentRightHandWeapon as MeleeWeaponItem;
                riposteCollider = player.playerEquipmentManager.rightWeaponManager.meleeDamageCollider;
            }

            //  THE RIPSOTE ANIMATION WILL CHANGE DEPENDING ON THE WEAPON'S ANIMATOR CONTROLLER, SO THE ANIMATION CAN BE CHOOSEN THERE, THE NAME WILL ALWAYS BE THE SAME
            character.characterAnimatorManager.PlayTargetActionAnimationInstantly("Riposte_01", true);

            //  WHILST PERFORMING A CRITICAL STRIKE, YOU CANNOT BE DAMAGED
            if (character.IsOwner)
                character.characterNetworkManager.isInvulnerable.Value = true;

            // 1. CREATE A NEW DAMAGE EFFECT FOR THIS TYPE OF DAMAGE
            TakeCriticalDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeCriticalDamageEffect);

            // 2. APPLY ALL OF THE DAMAGE STATS FROM THE COLLIDER TO THE DAMAGE EFFECT
            damageEffect.physicalDamage = riposteCollider.physicalDamage;
            damageEffect.holyDamage = riposteCollider.holyDamage;
            damageEffect.fireDamage = riposteCollider.fireDamage;
            damageEffect.lightningDamage = riposteCollider.lightningDamage;
            damageEffect.magicDamage = riposteCollider.magicDamage;
            damageEffect.poiseDamage = riposteCollider.poiseDamage;

            // 3. MULTIPLY DAMAGE BY WEAPONS RIPOSTE MODIFIER
            damageEffect.physicalDamage *= riposteWeapon.riposte_Attack_01_Modifier;
            damageEffect.holyDamage *= riposteWeapon.riposte_Attack_01_Modifier;
            damageEffect.fireDamage *= riposteWeapon.riposte_Attack_01_Modifier;
            damageEffect.lightningDamage *= riposteWeapon.riposte_Attack_01_Modifier;
            damageEffect.magicDamage *= riposteWeapon.riposte_Attack_01_Modifier;
            damageEffect.poiseDamage *= riposteWeapon.riposte_Attack_01_Modifier;

            // 4. USING A SERVER RPC SEND THE RIPOSTE TO THE TARGET, WHERE THEY WILL PLAY THE PROPER ANIMATIONS ON THEIR END, AND TAKE THE DAMAGE
            targetCharacter.characterNetworkManager.NotifyTheServerOfRiposteServerRpc(
                targetCharacter.NetworkObjectId,
                character.NetworkObjectId,
                "Riposted_01",
                riposteWeapon.itemID,
                damageEffect.physicalDamage,
                damageEffect.magicDamage,
                damageEffect.fireDamage,
                damageEffect.holyDamage,
                damageEffect.poiseDamage);
        }

        public override void AttemptBackstab(RaycastHit hit)
        {
            CharacterManager targetCharacter = hit.transform.gameObject.GetComponent<CharacterManager>();

            //  IF FOR SOME REASON THE TARGET CHARACTER IS NULL, RETURN
            if (targetCharacter == null)
                return;

            //  IF SOME HOW SINCE THE INITIAL CHECK THE CHARACTER CAN NO LONGER BE RIPOSTED, RETURN
            if (!targetCharacter.characterCombatManager.canBeBackstabbed)
                return;

            //  IF SOMEBODY ELSE IS ALREADY PERFORMING A CRITICAL STRIKE ON THE CHARACTER (OR WE ALREADY ARE), RETURN
            if (targetCharacter.characterNetworkManager.isBeingCriticallyDamaged.Value)
                return;

            //  YOU CAN ONLY RIPOSTE WITH A MELEE WEAPON ITEM
            MeleeWeaponItem backstabWeapon;
            MeleeWeaponDamageCollider backstabCollider;

            //  TODO: CHECK IF WE ARE TWO HANDING LEFT WEAPON OR RIGHT WEAPON (THIS WILL CHANGE THE RIPOSTE WEAPON)

            if (player.playerNetworkManager.isTwoHandingLeftWeapon.Value)
            {
                backstabWeapon = player.playerInventoryManager.currentLeftHandWeapon as MeleeWeaponItem;
                backstabCollider = player.playerEquipmentManager.leftWeaponManager.meleeDamageCollider;
            }
            else
            {
                backstabWeapon = player.playerInventoryManager.currentRightHandWeapon as MeleeWeaponItem;
                backstabCollider = player.playerEquipmentManager.rightWeaponManager.meleeDamageCollider;
            }

            //  THE RIPSOTE ANIMATION WILL CHANGE DEPENDING ON THE WEAPON'S ANIMATOR CONTROLLER, SO THE ANIMATION CAN BE CHOOSEN THERE, THE NAME WILL ALWAYS BE THE SAME
            character.characterAnimatorManager.PlayTargetActionAnimationInstantly("Backstab_01", true);

            //  WHILST PERFORMING A CRITICAL STRIKE, YOU CANNOT BE DAMAGED
            if (character.IsOwner)
                character.characterNetworkManager.isInvulnerable.Value = true;

            // 1. CREATE A NEW DAMAGE EFFECT FOR THIS TYPE OF DAMAGE
            TakeCriticalDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeCriticalDamageEffect);

            // 2. APPLY ALL OF THE DAMAGE STATS FROM THE COLLIDER TO THE DAMAGE EFFECT
            damageEffect.physicalDamage = backstabCollider.physicalDamage;
            damageEffect.holyDamage = backstabCollider.holyDamage;
            damageEffect.fireDamage = backstabCollider.fireDamage;
            damageEffect.lightningDamage = backstabCollider.lightningDamage;
            damageEffect.magicDamage = backstabCollider.magicDamage;
            damageEffect.poiseDamage = backstabCollider.poiseDamage;

            // 3. MULTIPLY DAMAGE BY WEAPONS RIPOSTE MODIFIER
            damageEffect.physicalDamage *= backstabWeapon.backstab_Attack_01_Modifier;
            damageEffect.holyDamage *= backstabWeapon.backstab_Attack_01_Modifier;
            damageEffect.fireDamage *= backstabWeapon.backstab_Attack_01_Modifier;
            damageEffect.lightningDamage *= backstabWeapon.backstab_Attack_01_Modifier;
            damageEffect.magicDamage *= backstabWeapon.backstab_Attack_01_Modifier;
            damageEffect.poiseDamage *= backstabWeapon.backstab_Attack_01_Modifier;

            // 4. USING A SERVER RPC SEND THE RIPOSTE TO THE TARGET, WHERE THEY WILL PLAY THE PROPER ANIMATIONS ON THEIR END, AND TAKE THE DAMAGE
            targetCharacter.characterNetworkManager.NotifyTheServerOfBackstabServerRpc(
                targetCharacter.NetworkObjectId,
                character.NetworkObjectId,
                "Backstabbed_01",
                backstabWeapon.itemID,
                damageEffect.physicalDamage,
                damageEffect.magicDamage,
                damageEffect.fireDamage,
                damageEffect.holyDamage,
                damageEffect.poiseDamage);
        }

        public virtual void DrainStaminaBasedOnAttack()
        {
            if (!player.IsOwner)
                return;

            if (currentWeaponBeingUsed == null)
                return;

            float staminaDeducted = 0;

            switch (currentAttackType)
            {
                case AttackType.LightAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.LightAttack02:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.HeavyAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.heavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.HeavyAttack02:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.heavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.ChargedAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.chargedAttackStaminaCostMultiplier;
                    break;
                case AttackType.ChargedAttack02:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.chargedAttackStaminaCostMultiplier;
                    break;
                case AttackType.RunningAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.runningAttackStaminaCostMultiplier;
                    break;
                case AttackType.RollingAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.rollingAttackStaminaCostMultiplier;
                    break;
                case AttackType.BackstepAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.backstepAttackStaminaCostMultiplier;
                    break;
                default:
                    break;
            }

            player.playerNetworkManager.currentStamina.Value -= Mathf.RoundToInt(staminaDeducted);
        }

        public override void SetTarget(CharacterManager newTarget)
        {
            base.SetTarget(newTarget);

            if (player.IsOwner)
            {
                PlayerCamera.instance.SetLockCameraHeight();
            }
        }

        //  ANIMATION EVENT CALLS
        public override void EnableCanDoCombo()
        {
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                player.playerCombatManager.canComboWithMainHandWeapon = true;
            }
            else
            {
                //  ENABLE OFF HAND COMBO
            }
        }

        public override void DisableCanDoCombo()
        {
            player.playerCombatManager.canComboWithMainHandWeapon = false;
            //player.playerCombatManager.canComboWithOffHandWeapon = false;
        }

        public WeaponItem SelectWeaponToPerformAshOfWar()
        {
            //  TO DO SELECT WEAPON DEPENDING ON SETUP
            WeaponItem selectedWeapon = player.playerInventoryManager.currentLeftHandWeapon;
            player.playerNetworkManager.SetCharacterActionHand(false);
            player.playerNetworkManager.currentWeaponBeingUsed.Value = selectedWeapon.itemID;
            return selectedWeapon;
        }
    }
}
