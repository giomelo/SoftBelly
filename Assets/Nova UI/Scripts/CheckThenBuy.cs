using _Scripts.U_Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckThenBuy : MonoBehaviour
{
    [SerializeField]
    GameObject popup;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    Text valString;

    public void StartPurchase()
    {
        float val = float.Parse(valString.text.Substring(2));
        if (val >= UniversalVariables.Instance.Money)
        {
            if (popup && !popup.activeInHierarchy) { popup.SetActive(true); }
        }
        else
        {
            //Colocar aviso de DINHEIRO INSUFICIENTE aqui
        }
    }

    public void WaitThenHide()
    {
        StartCoroutine("WaitsThenHides");
    }

    private IEnumerator WaitsThenHides()
    {
        fade.GetComponent<Animator>().Play("FadeAnim.FadeOut");
        popup.GetComponent<Animator>().Play("PurchaseAnim.HideMenu");
        for (int i = 0; i < 30; i++) { yield return new WaitForFixedUpdate(); }
        popup.SetActive(false);
        fade.SetActive(false);
    }
}
