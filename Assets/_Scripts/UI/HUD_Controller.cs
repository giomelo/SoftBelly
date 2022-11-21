using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using _Scripts.Entities.Player;
using _Scripts.Singleton;
using _Scripts.U_Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class HUD_Controller : MonoSingleton<HUD_Controller>
    {
        [SerializeField]
        private TextMeshProUGUI moneyText;
        [SerializeField]
        private TextMeshProUGUI reputationText;
        [SerializeField]
        private TextMeshProUGUI nivelText;
        [SerializeField]
        private TextMeshProUGUI hoursText;
        [SerializeField]
        private TextMeshProUGUI secondsText;
        [SerializeField]
        private GameObject confirmBuy;
        private Animator anim;
        public int popupOption = 0;

        [SerializeField]
        private Slider reputaionSlider;

        public TextMeshProUGUI diaText;
        public Animation animm;
        public TextMeshProUGUI levelText;
        public Animation levelAnim;

        [SerializeField] private Slider aligmentSlieder;

        public void DiaDisplay()
        {
            diaText.text = DaysController.Instance.currentDay.ToString();
            animm.Play();
        }
        private void Start()
        {
            reputaionSlider.maxValue = 500;
            aligmentSlieder.maxValue = 100;
            UpdateMoneyText();
            UpdateReputationText();
            UpdateNivel();
            UpdateAlignment();
            anim = confirmBuy.GetComponent <Animator> ();
        }

        public void ShowBuyPopup(Action<bool, int, bool, bool> callback, int id, bool plot)
        {
            StartCoroutine(_Showbuypopup(callback, id, plot));
        }

        private IEnumerator _Showbuypopup(Action<bool, int, bool, bool> callback, int id, bool plot)
        {
            confirmBuy.SetActive(true);

            popupOption = 0;

            while (popupOption == 0)
                yield return new WaitForEndOfFrame();

            //close canvas
            anim.Play("PurchaseAnim.HideMenu");
            yield return new WaitForSeconds(0.6f);
            confirmBuy.SetActive(false);
            
            callback?.Invoke(popupOption != 2, id, plot, true);
        }
        
        public void SetPopupOption(int option)
        {
            popupOption = option;
            PlayerInputHandler.EnableInputCall();
        }

        public void UpdateMoneyText()
        {
            moneyText.text = UniversalVariables.Instance.Money.ToString();
        }
        
        
        public void UpdateReputationText()
        {
            reputationText.text =  UniversalVariables.Instance.Reputation.ToString();
            reputaionSlider.value = UniversalVariables.Instance.Reputation;
        }
        public void UpdateAlignment()
        {
            aligmentSlieder.value = UniversalVariables.Instance.SocialAlignment;
        }
        public void UpdateNivel()
        {
            var aux = Math.Ceiling((UniversalVariables.Instance.Nivel / 100));
            if (aux.ToString(CultureInfo.InvariantCulture) == nivelText.text) return;
            nivelText.text = aux.ToString(CultureInfo.InvariantCulture);
            if(aux != 1) LevelUp();
        }

        private void LevelUp()
        {
            GameManager.Instance.PromotionLevelCall();
            levelText.text = "Subiu de Nível!";
            levelAnim.Play();
        }
        public void EndDay()
        {
            levelText.text = "Não há mais pacientes para chegar!";
            levelAnim.Play();
        }
        public void AvisoCama()
        {
            levelText.text = "Está muito cedo para dormir!";
            levelAnim.Play();
        }
        public void AvisoDromir()
        {
            levelText.text = "Está ficando tarde é melhor dormir parar terminar o dia!";
            levelAnim.Play();
        }

        public void UpdateTimeText()
        {
            hoursText.text= DaysController.Instance.time.Hours.ToString("").PadLeft(2, '0');
            secondsText.text= DaysController.Instance.time.Minutes.ToString("").PadLeft(2, '0');
        }
    }
}
