using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string scene;
    private void OnTriggerEnter(Collider other)
    {
        ScreenFlow.Instance.LoadScene(scene);
    }
}
