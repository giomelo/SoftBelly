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

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            UpdateMoneyText();
        }

        public void UpdateMoneyText()
        {
            moneyText.text = UniversalVariables.Money.ToString();
        }
    }
}
