using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.U_Variables;
using TMPro;
using UnityEngine;

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
        public int popupOption = 0;

        private void Start()
        {
            UpdateMoneyText();
            UpdateReputationText();
            UpdateNivel();
        }

        public void ShowBuyPopup(Action<bool, int, bool> callback, int id, bool plot)
        {
            StartCoroutine(_Showbuypopup(callback, id, plot));
        }

        private IEnumerator _Showbuypopup(Action<bool, int, bool> callback, int id, bool plot)
        {
            confirmBuy.SetActive(true);

            popupOption = 0;

            while (popupOption == 0)
                yield return new WaitForEndOfFrame();
            
            //close canvas
            confirmBuy.SetActive(false);
            
            callback?.Invoke(popupOption != 2, id, plot);
        }
        
        public void SetPopupOption(int option) => popupOption = option;

        public void UpdateMoneyText()
        {
            moneyText.text = UniversalVariables.Instance.Money.ToString();
        }
        
        
        public void UpdateReputationText()
        {
            reputationText.text =  UniversalVariables.Instance.Reputation.ToString();
        }
        public void UpdateNivel()
        {
            
            var aux = Math.Ceiling(UniversalVariables.Instance.Nivel / 100);
            if (aux.ToString() == nivelText.text) return;
            GameManager.Instance.PromotionLevelCall();
            nivelText.text = aux.ToString();

        }

        public void UpdateTimeText()
        {
            hoursText.text= DaysController.Instance.time.Hours.ToString("").PadLeft(2, '0');
            secondsText.text= DaysController.Instance.time.Minutes.ToString("").PadLeft(2, '0');
        }
    }
}
