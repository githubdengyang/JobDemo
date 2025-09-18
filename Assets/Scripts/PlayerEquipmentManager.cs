using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;

        [Header("Weapon Model Instantiation Slots")]
        [HideInInspector] public WeaponModelInstantiationSlot rightHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandShieldSlot;
        [HideInInspector] public WeaponModelInstantiationSlot backSlot;

        [Header("Weapon Models")]
        [HideInInspector] public GameObject rightHandWeaponModel;
        [HideInInspector] public GameObject leftHandWeaponModel;

        [Header("Weapon Managers")]
        public WeaponManager rightWeaponManager;
        public WeaponManager leftWeaponManager;

        [Header("DEBUG DELETE LATER")]
        [SerializeField] bool equipNewItems = false;

        [Header("General Equipment Models")]
        public GameObject hatsObject;
        [HideInInspector] public GameObject[] hats;
        public GameObject hoodsObject;
        [HideInInspector] public GameObject[] hoods;
        public GameObject faceCoversObject;
        [HideInInspector] public GameObject[] faceCovers;
        public GameObject helmetAccessoriesObject;
        [HideInInspector] public GameObject[] helmetAccessories;
        public GameObject backAccessoriesObject;
        [HideInInspector] public GameObject[] backAccessories;
        public GameObject hipAccessoriesObject;
        [HideInInspector] public GameObject[] hipAccessories;
        public GameObject rightShoulderObject;
        [HideInInspector] public GameObject[] rightShoulder;
        public GameObject rightElbowObject;
        [HideInInspector] public GameObject[] rightElbow;
        public GameObject rightKneeObject;
        [HideInInspector] public GameObject[] rightKnee;
        public GameObject leftShoulderObject;
        [HideInInspector] public GameObject[] leftShoulder;
        public GameObject leftElbowObject;
        [HideInInspector] public GameObject[] leftElbow;
        public GameObject leftKneeObject;
        [HideInInspector] public GameObject[] leftKnee;

        [Header("Male Equipment Models")]
        public GameObject maleFullHelmetObject;
        [HideInInspector] public GameObject[] maleHeadFullHelmets;
        public GameObject maleFullBodyObject;
        [HideInInspector] public GameObject[] maleBodies;
        public GameObject maleRightUpperArmObject;
        [HideInInspector] public GameObject[] maleRightUpperArms;
        public GameObject maleRightLowerArmObject;
        [HideInInspector] public GameObject[] maleRightLowerArms;
        public GameObject maleRightHandObject;
        [HideInInspector] public GameObject[] maleRightHands;
        public GameObject maleLeftUpperArmObject;
        [HideInInspector] public GameObject[] maleLeftUpperArms;
        public GameObject maleLeftLowerArmObject;
        [HideInInspector] public GameObject[] maleLeftLowerArms;
        public GameObject maleLeftHandObject;
        [HideInInspector] public GameObject[] maleLeftHands;
        public GameObject maleHipsObject;
        [HideInInspector] public GameObject[] maleHips;
        public GameObject maleRightLegObject;
        [HideInInspector] public GameObject[] maleRightLegs;
        public GameObject maleLeftLegObject;
        [HideInInspector] public GameObject[] maleLeftLegs;

        [Header("Female Equipment Models")]
        public GameObject femaleFullHelmetObject;
        [HideInInspector] public GameObject[] femaleHeadFullHelmets;
        public GameObject femaleFullBodyObject;
        [HideInInspector] public GameObject[] femaleBodies;
        public GameObject femaleRightUpperArmObject;
        [HideInInspector] public GameObject[] femaleRightUpperArms;
        public GameObject femaleRightLowerArmObject;
        [HideInInspector] public GameObject[] femaleRightLowerArms;
        public GameObject femaleRightHandObject;
        [HideInInspector] public GameObject[] femaleRightHands;
        public GameObject femaleLeftUpperArmObject;
        [HideInInspector] public GameObject[] femaleLeftUpperArms;
        public GameObject femaleLeftLowerArmObject;
        [HideInInspector] public GameObject[] femaleLeftLowerArms;
        public GameObject femaleLeftHandObject;
        [HideInInspector] public GameObject[] femaleLeftHands;
        public GameObject femaleHipsObject;
        [HideInInspector] public GameObject[] femaleHips;
        public GameObject femaleRightLegObject;
        [HideInInspector] public GameObject[] femaleRightLegs;
        public GameObject femaleLeftLegObject;
        [HideInInspector] public GameObject[] femaleLeftLegs;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();

            InitializeWeaponSlots();
            InitializeArmorModels();
        }

        protected override void Start()
        {
            base.Start();

            EquipWeapons();
        }

        private void Update()
        {
            if (equipNewItems)
            {
                equipNewItems = false;
                EquipArmor();
            }
        }

        public void EquipArmor()
        {
            LoadHeadEquipment(player.playerInventoryManager.headEquipment);
            LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);
            LoadLegEquipment(player.playerInventoryManager.legEquipment);
            LoadHandEquipment(player.playerInventoryManager.handEquipment);
        }

        //  EQUIPMENT
        private void InitializeArmorModels()
        {
            //  HATS
            List<GameObject> hatsList = new List<GameObject>();

            foreach (Transform child in hatsObject.transform)
            {
                hatsList.Add(child.gameObject);
            }

            hats = hatsList.ToArray();

            //  HOODS
            List<GameObject> hoodsList = new List<GameObject>();

            foreach (Transform child in hoodsObject.transform)
            {
                hoodsList.Add(child.gameObject);
            }

            hoods = hoodsList.ToArray();

            //  FACE COVERS
            List<GameObject> faceCoversList = new List<GameObject>();

            foreach (Transform child in faceCoversObject.transform)
            {
                faceCoversList.Add(child.gameObject);
            }

            faceCovers = faceCoversList.ToArray();

            //  HELMET ACCESSORIES
            List<GameObject> helmetAccessoriesList = new List<GameObject>();

            foreach (Transform child in helmetAccessoriesObject.transform)
            {
                helmetAccessoriesList.Add(child.gameObject);
            }

            helmetAccessories = helmetAccessoriesList.ToArray();

            //  BACK ACCESSORIES
            List<GameObject> backAccessoriesList = new List<GameObject>();

            foreach (Transform child in backAccessoriesObject.transform)
            {
                backAccessoriesList.Add(child.gameObject);
            }

            backAccessories = backAccessoriesList.ToArray();

            //  HIP ACCESSORIES
            List<GameObject> hipAccessoriesList = new List<GameObject>();

            foreach (Transform child in hipAccessoriesObject.transform)
            {
                hipAccessoriesList.Add(child.gameObject);
            }

            hipAccessories = hipAccessoriesList.ToArray();

            //  RIGHT SHOULDER
            List<GameObject> rightShoulderList = new List<GameObject>();

            foreach (Transform child in rightShoulderObject.transform)
            {
                rightShoulderList.Add(child.gameObject);
            }

            rightShoulder = rightShoulderList.ToArray();

            //  RIGHT ELBOW
            List<GameObject> rightElbowList = new List<GameObject>();

            foreach (Transform child in rightElbowObject.transform)
            {
                rightElbowList.Add(child.gameObject);
            }

            rightElbow = rightElbowList.ToArray();

            //  RIGHT KNEE
            List<GameObject> rightKneeList = new List<GameObject>();

            foreach (Transform child in rightKneeObject.transform)
            {
                rightKneeList.Add(child.gameObject);
            }

            rightKnee = rightKneeList.ToArray();

            //  LEFT SHOULDER
            List<GameObject> leftShoulderList = new List<GameObject>();

            foreach (Transform child in leftShoulderObject.transform)
            {
                leftShoulderList.Add(child.gameObject);
            }

            leftShoulder = leftShoulderList.ToArray();

            //  LEFT ELBOW
            List<GameObject> leftElbowList = new List<GameObject>();

            foreach (Transform child in leftElbowObject.transform)
            {
                leftElbowList.Add(child.gameObject);
            }

            leftElbow = leftElbowList.ToArray();

            //  LEFT KNEE
            List<GameObject> leftKneeList = new List<GameObject>();

            foreach (Transform child in leftKneeObject.transform)
            {
                leftKneeList.Add(child.gameObject);
            }

            leftKnee = leftKneeList.ToArray();

            //  MALE EQUIPMENT

            List<GameObject> maleFullHelmetsList = new List<GameObject>();

            foreach (Transform child in maleFullHelmetObject.transform)
            {
                maleFullHelmetsList.Add(child.gameObject);
            }

            maleHeadFullHelmets = maleFullHelmetsList.ToArray();

            List<GameObject> maleBodiesList = new List<GameObject>();

            foreach (Transform child in maleFullBodyObject.transform)
            {
                maleBodiesList.Add(child.gameObject);
            }

            maleBodies = maleBodiesList.ToArray();

            //  MALE RIGHT UPPER ARM
            List<GameObject> maleRightUpperArmList = new List<GameObject>();

            foreach (Transform child in maleRightUpperArmObject.transform)
            {
                maleRightUpperArmList.Add(child.gameObject);
            }

            maleRightUpperArms = maleRightUpperArmList.ToArray();

            //  MALE RIGHT LOWER ARM
            List<GameObject> maleRightLowerArmList = new List<GameObject>();

            foreach (Transform child in maleRightLowerArmObject.transform)
            {
                maleRightLowerArmList.Add(child.gameObject);
            }

            maleRightLowerArms = maleRightLowerArmList.ToArray();

            //  MALE RIGHT HANDS
            List<GameObject> maleRightHandsList = new List<GameObject>();

            foreach (Transform child in maleRightHandObject.transform)
            {
                maleRightHandsList.Add(child.gameObject);
            }

            maleRightHands = maleRightHandsList.ToArray();

            //  MALE LEFT UPPER ARM
            List<GameObject> maleLeftUpperArmList = new List<GameObject>();

            foreach (Transform child in maleLeftUpperArmObject.transform)
            {
                maleLeftUpperArmList.Add(child.gameObject);
            }

            maleLeftUpperArms = maleLeftUpperArmList.ToArray();

            //  MALE LEFT LOWER ARM
            List<GameObject> maleLeftLowerArmList = new List<GameObject>();

            foreach (Transform child in maleLeftLowerArmObject.transform)
            {
                maleLeftLowerArmList.Add(child.gameObject);
            }

            maleLeftLowerArms = maleLeftLowerArmList.ToArray();

            //  MALE LEFT HANDS
            List<GameObject> maleLeftHandsList = new List<GameObject>();

            foreach (Transform child in maleLeftHandObject.transform)
            {
                maleLeftHandsList.Add(child.gameObject);
            }

            maleLeftHands = maleLeftHandsList.ToArray();

            //  MALE HIPS
            List<GameObject> maleHipsList = new List<GameObject>();

            foreach (Transform child in maleHipsObject.transform)
            {
                maleHipsList.Add(child.gameObject);
            }

            maleHips = maleHipsList.ToArray();

            //  MALE RIGHT LEG
            List<GameObject> maleRightLegList = new List<GameObject>();

            foreach (Transform child in maleRightLegObject.transform)
            {
                maleRightLegList.Add(child.gameObject);
            }

            maleRightLegs = maleRightLegList.ToArray();

            //  MALE LEFT LEG
            List<GameObject> maleLeftLegList = new List<GameObject>();

            foreach (Transform child in maleLeftLegObject.transform)
            {
                maleLeftLegList.Add(child.gameObject);
            }

            maleLeftLegs = maleLeftLegList.ToArray();

            //  FEMALE FULL HELMETS
            List<GameObject> femaleFullHelmetsList = new List<GameObject>();

            foreach (Transform child in femaleFullHelmetObject.transform)
            {
                femaleFullHelmetsList.Add(child.gameObject);
            }

            femaleHeadFullHelmets = femaleFullHelmetsList.ToArray();

            //  FEMALE BODY
            List<GameObject> femaleBodyList = new List<GameObject>();

            foreach (Transform child in femaleFullBodyObject.transform)
            {
                femaleBodyList.Add(child.gameObject);
            }

            femaleBodies = femaleBodyList.ToArray();

            //  FEMALE RIGHT UPPER ARM
            List<GameObject> femaleRightUpperArmList = new List<GameObject>();

            foreach (Transform child in femaleRightUpperArmObject.transform)
            {
                femaleRightUpperArmList.Add(child.gameObject);
            }

            femaleRightUpperArms = femaleRightUpperArmList.ToArray();

            //  FEMALE RIGHT LOWER ARM
            List<GameObject> femaleRightLowerArmList = new List<GameObject>();

            foreach (Transform child in femaleRightLowerArmObject.transform)
            {
                femaleRightLowerArmList.Add(child.gameObject);
            }

            femaleRightLowerArms = femaleRightLowerArmList.ToArray();

            //  FEMALE RIGHT HANDS
            List<GameObject> femaleRightHandsList = new List<GameObject>();

            foreach (Transform child in femaleRightHandObject.transform)
            {
                femaleRightHandsList.Add(child.gameObject);
            }

            femaleRightHands = femaleRightHandsList.ToArray();

            //  FEMALE LEFT UPPER ARM
            List<GameObject> femaleLeftUpperArmList = new List<GameObject>();

            foreach (Transform child in femaleLeftUpperArmObject.transform)
            {
                femaleLeftUpperArmList.Add(child.gameObject);
            }

            femaleLeftUpperArms = femaleLeftUpperArmList.ToArray();

            //  FEMALE LEFT LOWER ARM
            List<GameObject> femaleLeftLowerArmList = new List<GameObject>();

            foreach (Transform child in femaleLeftLowerArmObject.transform)
            {
                femaleLeftLowerArmList.Add(child.gameObject);
            }

            femaleLeftLowerArms = femaleLeftLowerArmList.ToArray();

            //  FEMALE LEFT HANDS
            List<GameObject> femaleLeftHandsList = new List<GameObject>();

            foreach (Transform child in femaleLeftHandObject.transform)
            {
                femaleLeftHandsList.Add(child.gameObject);
            }

            femaleLeftHands = femaleLeftHandsList.ToArray();

            //  FEMALE HIPS
            List<GameObject> femaleHipsList = new List<GameObject>();

            foreach (Transform child in femaleHipsObject.transform)
            {
                femaleHipsList.Add(child.gameObject);
            }

            femaleHips = femaleHipsList.ToArray();

            //  FEMALE RIGHT LEG
            List<GameObject> femaleRightLegList = new List<GameObject>();

            foreach (Transform child in femaleRightLegObject.transform)
            {
                femaleRightLegList.Add(child.gameObject);
            }

            femaleRightLegs = femaleRightLegList.ToArray();

            //  FEMALE LEFT LEG
            List<GameObject> femaleLeftLegList = new List<GameObject>();

            foreach (Transform child in femaleLeftLegObject.transform)
            {
                femaleLeftLegList.Add(child.gameObject);
            }

            femaleLeftLegs = femaleLeftLegList.ToArray();
        }

        public void LoadHeadEquipment(HeadEquipmentItem equipment)
        {
            //  1. UNLOAD OLD HEAD EQUIPMENT MODELS (IF ANY)
            UnloadHeadEquipmentModels();

            //  2. IF EQUIPMENT IS NULL SIMPLY SET EQUIPMENT IN INVENTORY TO NULL AND RETURN
            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.headEquipmentID.Value = -1; //  -1 WILL NEVER BE AN ITEM ID, SO IT WILL ALWAYS BE NULL

                player.playerInventoryManager.headEquipment = null;
                return;
            }

            //  3. IF YOU HAVE AN "ONITEMEQUIPPED" CALL ON YOUR EQUIPMENT, RUN IT NOW

            //  4. SET CURRENT HEAD EQUIPMENT IN PLAYER INVENTORY TO THE EQUIPMENT THAT IS PASSED TO THIS FUNCTION
            player.playerInventoryManager.headEquipment = equipment;

            //  5. IF YOU NEED TO CHECK FOR HEAD EQUIPMENT TYPE TO DISABLE CERTAIN BODY FEATURES (HOODS DISABLING HAIR ECT, FULL HELMS DISABLING HEADS) DO IT NOW

            switch (equipment.headEquipmentType)
            {
                case HeadEquipmentType.FullHelmet:
                    player.playerBodyManager.DisableHair();
                    player.playerBodyManager.DisableHead();
                    break;
                case HeadEquipmentType.Hat:
                    break;
                case HeadEquipmentType.Hood:
                    player.playerBodyManager.DisableHair();
                    break;
                case HeadEquipmentType.FaceCover:
                    player.playerBodyManager.DisableFacialHair();
                    break;
                default:
                    break;
            }
            //  6. LOAD HEAD EQUIPMENT MODELS
            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            //  7. CALCULATE TOTAL EQUIPMENT LOAD (WEIGHT OF ALL YOUR WORN EQUIPMENT. THIS IMPACTS ROLL SPEED AND AT EXTREME WEIGHTS, MOVEMENT SPEED)

            //  8. CALCULATE TOTAL ARMOR ABSORPTION
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.headEquipmentID.Value = equipment.itemID;
        }

        private void UnloadHeadEquipmentModels()
        {
            foreach (var model in maleHeadFullHelmets)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleHeadFullHelmets)
            {
                model.SetActive(false);
            }

            foreach (var model in hats)
            {
                model.SetActive(false);
            }

            foreach (var model in faceCovers)
            {
                model.SetActive(false);
            }

            foreach (var model in hoods)
            {
                model.SetActive(false);
            }

            foreach (var model in helmetAccessories)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableHead();
            player.playerBodyManager.EnableHair();
        }

        public void LoadBodyEquipment(BodyEquipmentItem equipment)
        {
            //  1. UNLOAD OLD EQUIPMENT MODELS (IF ANY)
            UnloadBodyEquipmentModels();

            //  2. IF EQUIPMENT IS NULL SIMPLY SET EQUIPMENT IN INVENTORY TO NULL AND RETURN
            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.bodyEquipmentID.Value = -1; //  -1 WILL NEVER BE AN ITEM ID, SO IT WILL ALWAYS BE NULL

                player.playerInventoryManager.bodyEquipment = null;
                return;
            }

            //  3. IF YOU HAVE AN "ONITEMEQUIPPED" CALL ON YOUR EQUIPMENT, RUN IT NOW

            //  4. SET CURRENT HEAD EQUIPMENT IN PLAYER INVENTORY TO THE EQUIPMENT THAT IS PASSED TO THIS FUNCTION
            player.playerInventoryManager.bodyEquipment = equipment;

            //  5. IF YOU NEED TO CHECK FOR HEAD EQUIPMENT TYPE TO DISABLE CERTAIN BODY FEATURES (HOODS DISABLING HAIR ECT, FULL HELMS DISABLING HEADS) DO IT NOW
            player.playerBodyManager.DisableBody();

            //  6. LOAD HEAD EQUIPMENT MODELS
            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            //  7. CALCULATE TOTAL EQUIPMENT LOAD (WEIGHT OF ALL YOUR WORN EQUIPMENT. THIS IMPACTS ROLL SPEED AND AT EXTREME WEIGHTS, MOVEMENT SPEED)

            //  8. CALCULATE TOTAL ARMOR ABSORPTION
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.bodyEquipmentID.Value = equipment.itemID;
        }

        private void UnloadBodyEquipmentModels()
        {
            foreach (var model in rightShoulder)
            {
                model.SetActive(false);
            }

            foreach (var model in rightElbow)
            {
                model.SetActive(false);
            }


            foreach (var model in leftShoulder)
            {
                model.SetActive(false);
            }

            foreach (var model in leftElbow)
            {
                model.SetActive(false);
            }

            foreach (var model in backAccessories)
            {
                model.SetActive(false);
            }

            //  MALE
            foreach (var model in maleBodies)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightUpperArms)
            {
                model.SetActive(false);
            }

            foreach (var model in maleLeftUpperArms)
            {
                model.SetActive(false);
            }

            //  FEMALE
            foreach (var model in femaleBodies)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightUpperArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftUpperArms)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableBody();
        }

        public void LoadLegEquipment(LegEquipmentItem equipment)
        {
            //  1. UNLOAD OLD EQUIPMENT MODELS (IF ANY)
            UnloadLegEquipmentModels();

            //  2. IF EQUIPMENT IS NULL SIMPLY SET EQUIPMENT IN INVENTORY TO NULL AND RETURN
            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.legEquipmentID.Value = -1; //  -1 WILL NEVER BE AN ITEM ID, SO IT WILL ALWAYS BE NULL

                player.playerInventoryManager.legEquipment = null;
                return;
            }

            //  3. IF YOU HAVE AN "ONITEMEQUIPPED" CALL ON YOUR EQUIPMENT, RUN IT NOW

            //  4. SET CURRENT HEAD EQUIPMENT IN PLAYER INVENTORY TO THE EQUIPMENT THAT IS PASSED TO THIS FUNCTION
            player.playerInventoryManager.legEquipment = equipment;

            //  5. IF YOU NEED TO CHECK FOR HEAD EQUIPMENT TYPE TO DISABLE CERTAIN BODY FEATURES (HOODS DISABLING HAIR ECT, FULL HELMS DISABLING HEADS) DO IT NOW
            player.playerBodyManager.DisableLowerBody();

            //  6. LOAD HEAD EQUIPMENT MODELS
            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            //  7. CALCULATE TOTAL EQUIPMENT LOAD (WEIGHT OF ALL YOUR WORN EQUIPMENT. THIS IMPACTS ROLL SPEED AND AT EXTREME WEIGHTS, MOVEMENT SPEED)

            //  8. CALCULATE TOTAL ARMOR ABSORPTION
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.legEquipmentID.Value = equipment.itemID;
        }

        private void UnloadLegEquipmentModels()
        {
            foreach (var model in maleHips)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleHips)
            {
                model.SetActive(false);
            }

            foreach (var model in leftKnee)
            {
                model.SetActive(false);
            }

            foreach (var model in rightKnee)
            {
                model.SetActive(false);
            }

            foreach (var model in maleLeftLegs)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightLegs)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftLegs)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightLegs)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableLowerBody();
        }

        public void LoadHandEquipment(HandEquipmentItem equipment)
        {
            //  1. UNLOAD OLD EQUIPMENT MODELS (IF ANY)
            UnloadHandEquipmentModels();

            //  2. IF EQUIPMENT IS NULL SIMPLY SET EQUIPMENT IN INVENTORY TO NULL AND RETURN
            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.handEquipmentID.Value = -1; //  -1 WILL NEVER BE AN ITEM ID, SO IT WILL ALWAYS BE NULL

                player.playerInventoryManager.handEquipment = null;
                return;
            }

            //  3. IF YOU HAVE AN "ONITEMEQUIPPED" CALL ON YOUR EQUIPMENT, RUN IT NOW

            //  4. SET CURRENT HEAD EQUIPMENT IN PLAYER INVENTORY TO THE EQUIPMENT THAT IS PASSED TO THIS FUNCTION
            player.playerInventoryManager.handEquipment = equipment;

            //  5. IF YOU NEED TO CHECK FOR HEAD EQUIPMENT TYPE TO DISABLE CERTAIN BODY FEATURES (HOODS DISABLING HAIR ECT, FULL HELMS DISABLING HEADS) DO IT NOW
            player.playerBodyManager.DisableArms();

            //  6. LOAD HEAD EQUIPMENT MODELS
            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            //  7. CALCULATE TOTAL EQUIPMENT LOAD (WEIGHT OF ALL YOUR WORN EQUIPMENT. THIS IMPACTS ROLL SPEED AND AT EXTREME WEIGHTS, MOVEMENT SPEED)

            //  8. CALCULATE TOTAL ARMOR ABSORPTION
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.handEquipmentID.Value = equipment.itemID;
        }

        private void UnloadHandEquipmentModels()
        {
            foreach (var model in maleLeftLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in maleLeftHands)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightHands)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftHands)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightHands)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableArms();
        }

        //  WEAPONS
        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandWeaponSlot)
                {
                    leftHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandShieldSlot)
                {
                    leftHandShieldSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.BackSlot)
                {
                    backSlot = weaponSlot;
                }
            }
        }

        public void EquipWeapons()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }

        //  RIGHT WEAPON

        public void SwitchRightWeapon()
        {
            if (!player.IsOwner)
                return;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

            //  ELDEN RINGS WEAPON SWAPPING
            //  1. Check if we have another weapon besides our main weapon, if we do, NEVER swap to unarmed, rotate between weapon 1 and 2
            //  2. If we don't, swap to unarmed, then SKIP the other empty slot and swap back. Do not process both empty slots before returning to main weapon

            WeaponItem selectedWeapon = null;

            //  DISABLE TWO HANDING IF WE ARE TWO HANDING

            //  ADD ONE TO OUR INDEX TO SWITCH TO THE NEXT POTENTIAL WEAPON
            player.playerInventoryManager.rightHandWeaponIndex += 1;

            //  IF OUR INDEX IS OUT OF BOUNDS, RESET IT TO POSITION #1 (0)
            if (player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 2)
            {
                player.playerInventoryManager.rightHandWeaponIndex = 0;

                //  WE CHECK IF WE ARE HOLDING MORE THAN ONE WEAPON
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;
                for (int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsInRightHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;
                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsInRightHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }

                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.rightHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = firstWeapon.itemID;
                }

                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInRightHandSlots)
            {
                //  IF THE NEXT POTENTIAL WEAPON DOES NOT EQUAL THE UNARMED WEAPON
                if (player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex];
                    //  ASSIGN THE NETWORK WEAPON ID SO IT SWITCHES FOR ALL CONNECTED CLIENTS
                    player.playerNetworkManager.currentRightHandWeaponID.Value = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.rightHandWeaponIndex <= 2)
            {
                SwitchRightWeapon();
            }
        }

        public void LoadRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null)
            {
                //  REMOVE THE OLD WEAPON
                rightHandWeaponSlot.UnloadWeapon();

                //  BRING IN THE NEW WEAPON
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);
                rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
                player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);
            }
        }

        //  LEFT WEAPON

        public void SwitchLeftWeapon()
        {
            if (!player.IsOwner)
                return;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

            //  ELDEN RINGS WEAPON SWAPPING
            //  1. Check if we have another weapon besides our main weapon, if we do, NEVER swap to unarmed, rotate between weapon 1 and 2
            //  2. If we don't, swap to unarmed, then SKIP the other empty slot and swap back. Do not process both empty slots before returning to main weapon

            WeaponItem selectedWeapon = null;

            //  DISABLE TWO HANDING IF WE ARE TWO HANDING

            //  ADD ONE TO OUR INDEX TO SWITCH TO THE NEXT POTENTIAL WEAPON
            player.playerInventoryManager.leftHandWeaponIndex += 1;

            //  IF OUR INDEX IS OUT OF BOUNDS, RESET IT TO POSITION #1 (0)
            if (player.playerInventoryManager.leftHandWeaponIndex < 0 || player.playerInventoryManager.leftHandWeaponIndex > 2)
            {
                player.playerInventoryManager.leftHandWeaponIndex = 0;

                //  WE CHECK IF WE ARE HOLDING MORE THAN ONE WEAPON
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;
                for (int i = 0; i < player.playerInventoryManager.weaponsInLeftHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsInLeftHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;
                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsInLeftHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }

                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.leftHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.leftHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = firstWeapon.itemID;
                }

                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInLeftHandSlots)
            {
                //  IF THE NEXT POTENTIAL WEAPON DOES NOT EQUAL THE UNARMED WEAPON
                if (player.playerInventoryManager.weaponsInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex];
                    //  ASSIGN THE NETWORK WEAPON ID SO IT SWITCHES FOR ALL CONNECTED CLIENTS
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = player.playerInventoryManager.weaponsInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.leftHandWeaponIndex <= 2)
            {
                SwitchLeftWeapon();
            }
        }

        public void LoadLeftWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                //  REMOVE THE OLD WEAPON
                if (leftHandWeaponSlot.currentWeaponModel != null)
                    leftHandWeaponSlot.UnloadWeapon();

                if (leftHandShieldSlot.currentWeaponModel != null)
                    leftHandShieldSlot.UnloadWeapon();

                //  BRING IN THE NEW WEAPON
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);

                switch (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType)
                {
                    case WeaponModelType.Weapon:
                        leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    case WeaponModelType.Shield:
                        leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    default:
                        break;
                }

                leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        //  TWO HAND
        public void UnTwoHandWeapon()
        {
            //  UPDATE ANIMATOR CONTROLLER TO CURRENT MAIN HAND WEAPON
            player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);

            //  REMOVE THE STRENGTH BONUS (TWO HANDING A WEAPON MAKES YOUR STRENGTH LEVEL (STRENGTH + (STRENGTH * 0.5))

            //  UN-TWO HAND THE MODEL AND MOVE THE MODEL THAT ISNT BEING TWO HANDED BACK TO ITS HAND (IF THERE IS ANY)

            //  LEFT HAND
            if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Weapon)
            {
                leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }
            else if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Shield)
            {
                leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }

            //  RIGHT HAND
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            //  REFRESH THE DAMAGE COLLIDER CALCULATIONS (STRENGTH SCALING WOULD BE EFFECTED SINCE THE STRENGTH BONUS WAS REMOVED)
            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void TwoHandRightWeapon()
        {
            // CHECK FOR UNTWOHANDABLE ITEM (Like unarmed) IF WE ARE ATTEMPTING TO TWO HAND UNARMED, RETURN
            if (player.playerInventoryManager.currentRightHandWeapon == WorldItemDatabase.Instance.unarmedWeapon)
            {
                // IF WE ARE RETURNING AND NOT TWO HANDING THE WEAPON, RESET BOOL STATUS'S
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingRightWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }

                return;
            }

            // UPDATE ANIMATOR
            player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);

            // PLACE THE NON-TWO HANDED WEAPON MODEL IN THE BACK SLOT OR HIP SLOT
            backSlot.PlaceWeaponModelInUnequippedSlot(leftHandWeaponModel, player.playerInventoryManager.currentLeftHandWeapon.weaponClass, player);

            // ADD TWO HAND STRENGTH BONUS

            // PLACE THE TWO HANDED WEAPON MODEL IN THE MAIN (RIGHT HAND)
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void TwoHandLeftWeapon()
        {
            // CHECK FOR UNTWOHANDABLE ITEM (Like unarmed) IF WE ARE ATTEMPTING TO TWO HAND UNARMED, RETURN
            if (player.playerInventoryManager.currentLeftHandWeapon == WorldItemDatabase.Instance.unarmedWeapon)
            {
                // IF WE ARE RETURNING AND NOT TWO HANDING THE WEAPON, RESET BOOL STATUS'S
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingLeftWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }

                return;
            }

            // UPDATE ANIMATOR
            player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentLeftHandWeapon.weaponAnimator);

            // PLACE THE NON-TWO HANDED WEAPON MODEL IN THE BACK SLOT OR HIP SLOT
            backSlot.PlaceWeaponModelInUnequippedSlot(rightHandWeaponModel, player.playerInventoryManager.currentRightHandWeapon.weaponClass, player);

            // ADD TWO HAND STRENGTH BONUS

            // PLACE THE TWO HANDED WEAPON MODEL IN THE MAIN (RIGHT HAND)
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        //  DAMAGE COLLIDERS
        public void OpenDamageCollider()
        {
            //  OPEN RIGHT WEAPON DAMAGE COLLIDER
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentRightHandWeapon.whooshes));
            }
            //  OPEN LEFT WEAPON DAMAGE COLLIDER
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManager.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentLeftHandWeapon.whooshes));
            }

            //  PLAY WHOOSH SFX
        }

        public void CloseDamageCollider()
        {
            //  OPEN RIGHT WEAPON DAMAGE COLLIDER
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
            }
            //  OPEN LEFT WEAPON DAMAGE COLLIDER
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManager.meleeDamageCollider.DisableDamageCollider();
            }
        }
    }
}
