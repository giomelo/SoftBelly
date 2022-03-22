using System.Collections;
using System.Collections.Generic;
using _Scripts.Store;
using _Scripts.Systems.Item;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public static Store store;
    public int Price = 0;
    [SerializeField]
    private ItemBehaviour _item;
    public Text textoBtn;
    
    public void Start()
    {
        store = this;
        NameBtn();
    }

    private void NameBtn()
    {
        textoBtn.text = _item.ItemId + " - Price: " + Price;
    }

    public void ClickBuy()
    {
        if (UniversalVariables.Money >= Price)
        {
            UniversalVariables.Money -= Price; 
            ControllerMoneyTXT.controllerMoneyTxt.TxtMoney.text = "Money: " + UniversalVariables.Money;
            ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(false);
            StoreController.Instance.AddItem(_item);
        }
        else
        {
            ControllerMoneyTXT.controllerMoneyTxt.TxtAvisoSemMoney.text = "Insufficient money!";
            ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(true);
            StartCoroutine(EsperarAviso());
        }
    }

    private IEnumerator EsperarAviso()
    {
        yield return new WaitForSeconds(1f);
        ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(false);
    }
}
