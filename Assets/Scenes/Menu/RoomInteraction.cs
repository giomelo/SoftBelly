using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteraction : MonoBehaviour
{
    public Light luz;

    private void Start()
    {
        luz.intensity = 0;
    }

    public void OnMouseEnter()
    {
        luz.intensity = 1.5f;
    }
    public void OnMouseExit()
    {
        luz.intensity = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            luz.intensity = 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            luz.intensity = 0;
        }
    }
}