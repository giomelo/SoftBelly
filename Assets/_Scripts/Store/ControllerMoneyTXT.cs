using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Store
{
    public class ControllerMoneyTXT : MonoBehaviour
    {
        // Controlar o texto da grana na tela.
        public static ControllerMoneyTXT controllerMoneyTxt;
        public Text TxtMoney;
        public Text TxtAvisoSemMoney;
        public GameObject PainelAviso;

        public void Start()
        {
            controllerMoneyTxt = this;
            TxtMoney.text = "Money: " + UniversalVariables.Money;
        }
    }
}