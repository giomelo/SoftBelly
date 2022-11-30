using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderMax : MonoBehaviour
{
    [SerializeField]
    GameObject items;
    [SerializeField]
    float scrollSpeed = 500;
    Slider slider;

    private void OnEnable()
    {
        SetMax();
    }

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }

    public void SetMax()
    {
        if(items.transform.childCount >= 5) 
        {
            slider.maxValue = 160 * (items.transform.childCount - 4);
        }
    }
}
