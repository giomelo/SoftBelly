using System.Collections;
using System.Collections.Generic;
using _Scripts.Systems.Lab.Machines;
using UnityEngine;

public class TimerFaceScreen : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
