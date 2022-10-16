using _Scripts.U_Variables;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{
    [SerializeField]
    Text dosh;

    private void Start()
    {
        EGoodENoisNaoHave();
    }

    public void EGoodENoisNaoHave() //Se nóis hevasse nóis não tava aqui workando
    {
        dosh.text = "$ " + UniversalVariables.Instance.Money;
    }
}