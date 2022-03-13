using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public static Store store;
    public int Price = 0;
    public string Item = " ";
    public Text textoBtn;
    
    public void Start()
    {
        store = this;
        NameBtn();
    }

    void NameBtn()
    {
        textoBtn.text = Item + " - Price: " + Price;
        
    }

    public void ClickBuy()
    {
        if (UniversalVariables.Money >= Price)
        {
            UniversalVariables.Money -= Price; 
            ControllerMoneyTXT.controllerMoneyTxt.TxtMoney.text = "Money: " + UniversalVariables.Money;
            ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive((false));
        }
        else
        {
            ControllerMoneyTXT.controllerMoneyTxt.TxtAvisoSemMoney.text = "Insufficient money!";
            ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive((true));
            StartCoroutine(EsperarAviso());
        }
    }

    IEnumerator EsperarAviso()
    {
        yield return new WaitForSeconds(1f);
        ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(false);
    }
}
