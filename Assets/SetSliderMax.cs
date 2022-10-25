using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderMax : MonoBehaviour
{
    [SerializeField]
    GameObject items;

    private void OnEnable()
    {
        SetMax();
    }

    public void SetMax()
    {
        if(items.transform.childCount >= 5) 
        {
            Slider slider = gameObject.GetComponent<Slider>();
            slider.maxValue = 130 + 105 * (items.transform.childCount - 4);
        }

    }
}
