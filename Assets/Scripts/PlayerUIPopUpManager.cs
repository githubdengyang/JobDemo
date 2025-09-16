using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SG
{
    public class PlayerUIPopUpManager : MonoBehaviour
    {
        [Header("Message Pop Up")]
        [SerializeField] TextMeshProUGUI popUpMessageText;
        [SerializeField] GameObject popUpMessageGameObject;

        [Header("Item Pop Up")]
        [SerializeField] GameObject itemPopUpGameObject;
        [SerializeField] Image itemIcon;
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI itemAmount;

        //  IF YOU PLAN ON MAKING ALL OF THESE POPUPS FUNCTION IDENTICALLY, YOU COULD JUST MAKE 1 POP UP GAMEOBJECT AND CHANGE THE TEXT VALUES AS NEEDED
        //  INSTEAD OF MAKING SEVERAL DIFFERENT GROUPS FOR POP UP FUNCTIONALITY
        [Header("YOU DIED Pop Up")]
        [SerializeField] GameObject youDiedPopUpGameObject;
        [SerializeField] TextMeshProUGUI youDiedPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI youDiedPopUpText;
        [SerializeField] CanvasGroup youDiedPopUpCanvasGroup;   //  Allows us to set the alpha to fade over time

        [Header("BOSS DEFEATED Pop Up")]
        [SerializeField] GameObject bossDefeatedPopUpGameObject;
        [SerializeField] TextMeshProUGUI bossDefeatedPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI bossDefeatedPopUpText;
        [SerializeField] CanvasGroup bossDefeatedPopUpCanvasGroup;   //  Allows us to set the alpha to fade over time

        [Header("GRACE RESTORED Pop Up")]
        [SerializeField] GameObject graceRestoredPopUpGameObject;
        [SerializeField] TextMeshProUGUI graceRestoredPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI graceRestoredPopUpText;
        [SerializeField] CanvasGroup graceRestoredPopUpCanvasGroup;   //  Allows us to set the alpha to fade over time

        public void CloseAllPopUpWindows()
        {
            popUpMessageGameObject.SetActive(false);
            itemPopUpGameObject.SetActive(false);

            PlayerUIManager.instance.popUpWindowIsOpen = false;
        }

        public void SendPlayerMessagePopUp(string messageText)
        {
            PlayerUIManager.instance.popUpWindowIsOpen = true;
            popUpMessageText.text = messageText;
            popUpMessageGameObject.SetActive(true);
        }

        public void SendItemPopUp(Item item, int amount)
        {
            itemAmount.enabled = false;
            itemIcon.sprite = item.itemIcon;
            itemName.text = item.itemName;

            if (amount > 0)
            {
                itemAmount.enabled = true;
                itemAmount.text = "x" + amount.ToString();
            }

            itemPopUpGameObject.SetActive(true);
            PlayerUIManager.instance.popUpWindowIsOpen = true;
        }

        public void SendYouDiedPopUp()
        {
            //  ACTIVATE POST PROCESSING EFFECTS

            youDiedPopUpGameObject.SetActive(true);
            youDiedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(youDiedPopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(youDiedPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(youDiedPopUpCanvasGroup, 2, 5));
        }

        public void SendBossDefeatedPopUp(string bossDefeatedMessage)
        {
            bossDefeatedPopUpText.text = bossDefeatedMessage;
            bossDefeatedPopUpBackgroundText.text = bossDefeatedMessage;
            bossDefeatedPopUpGameObject.SetActive(true);
            bossDefeatedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(bossDefeatedPopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(bossDefeatedPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(bossDefeatedPopUpCanvasGroup, 2, 5));
        }

        public void SendGraceRestoredPopUp(string graceRestoredMessage)
        {
            graceRestoredPopUpText.text = graceRestoredMessage;
            graceRestoredPopUpBackgroundText.text = graceRestoredMessage;
            graceRestoredPopUpGameObject.SetActive(true);
            graceRestoredPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(graceRestoredPopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(graceRestoredPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(graceRestoredPopUpCanvasGroup, 2, 5));
        }

        private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount)
        {
            if (duration > 0f)
            {
                text.characterSpacing = 0;  //  RESETS OUR CHARACTER SPACING
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer = timer + Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));
                    yield return null;
                }
            }
        }

        private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration)
        {
            if (duration > 0)
            {
                canvas.alpha = 0;
                float timer = 0;
                yield return null;

                while (timer < duration)
                {
                    timer = timer + Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                    yield return null;
                }
            }

            canvas.alpha = 1;

            yield return null;
        }

        private IEnumerator WaitThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0)
            {
                while (delay > 0)
                {
                    delay = delay - Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer = timer + Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                    yield return null;
                }
            }

            canvas.alpha = 0;

            yield return null;
        }
    }
}
