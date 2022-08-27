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
        private TextMeshProUGUI hoursText;
        [SerializeField]
        private TextMeshProUGUI secondsText;

        private void Start()
        {
            UpdateMoneyText();
            UpdateReputationText();
        }

        public void UpdateMoneyText()
        {
            moneyText.text = UniversalVariables.Instance.Money.ToString();
        }
        
        
        public void UpdateReputationText()
        {
            reputationText.text = UniversalVariables.Instance.Reputation.ToString();
        }

        public void UpdateTimeText()
        {
            hoursText.text= DaysController.Instance.time.Hours.ToString("").PadLeft(2, '0');
            secondsText.text= DaysController.Instance.time.Minutes.ToString("").PadLeft(2, '0');
        }
    }
}
