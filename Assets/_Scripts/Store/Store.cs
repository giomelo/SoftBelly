using System.Collections;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Store;
using _Scripts.Systems.Item;
using _Scripts.U_Variables;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public int Price = 0;
    [SerializeField] private ItemBehaviour _item;
    public Text textoBtn;
    
    public void Start()
    {
        NameBtn();
        UpdatePrice();
    }

    private void NameBtn()
    {
        textoBtn.text = _item.ItemId + " - Price: " + _item.Price;
    }

    private void UpdatePrice()
    {
        Price = (int) _item.Price;
    }

    public void UpdateItem(ItemBehaviour newItem)
    {
        _item = newItem;
        
        NameBtn();
        UpdatePrice();
    }

    public void ClickBuy()
    {
        if (UniversalVariables.Money >= Price)
        {
            UniversalVariables.Instance.ModifyMoney(Price, false);
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
