using _Scripts.U_Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Systems.Item;
using _Scripts.Singleton;

public class CheckThenBuy : MonoBehaviour
{
    [SerializeField]
    GameObject popup;

    [SerializeField]
    GameObject NoMoneyPopup;

    [SerializeField]
    GameObject fade;

    [SerializeField]
    Text valString;

    public void StartPurchase()
    {
        float val = float.Parse(valString.text.Substring(2));
        if (val <= UniversalVariables.Instance.Money)
        {
            if (popup && !popup.activeInHierarchy) { popup.SetActive(true); }
        }
        else
        {
            StartCoroutine("NoMoney");
        }
    }

    public void WaitThenHide()
    {
        StartCoroutine("WaitsThenHides");
    }

    public void ReduceMoney()
    {
        ItemBehaviour item = GameObject.Find("Itens").GetComponent<NewStoreInterface>().info_item;
        if (item)
        {
            float val = float.Parse(valString.text.Substring(2));
            UniversalVariables.Instance.Money -= val;
            if(item.ItemType == _Scripts.Enums.ItemType.Seed)
                GameManager.Instance.plantStorage.Storage.AddItem(1, item);
            else
                GameManager.Instance.labStorage.Storage.AddItem(1, item);
        }
        else
        {
            Debug.LogError("O prefab não possui um item dentro de si!");
        }

    }

    private IEnumerator WaitsThenHides()
    {
        fade.GetComponent<Animator>().Play("FadeAnim.FadeOut");
        popup.GetComponent<Animator>().Play("PurchaseAnim.HideMenu");
        for (int i = 0; i < 30; i++) { yield return new WaitForFixedUpdate(); }
        popup.SetActive(false);
        fade.SetActive(false);
    }

    private IEnumerator NoMoney()
    {
        fade.SetActive(true);
        NoMoneyPopup.SetActive(true);
        for (int i = 0; i < 140; i++) { yield return new WaitForFixedUpdate(); }
        fade.GetComponent<Animator>().Play("FadeAnim.FadeOut");
        for (int i = 0; i < 30; i++) { yield return new WaitForFixedUpdate(); }
        fade.SetActive(false);
        NoMoneyPopup.SetActive(false);
    }
}
