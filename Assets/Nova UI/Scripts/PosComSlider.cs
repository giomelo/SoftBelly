using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosComSlider : MonoBehaviour
{
    Vector3 vec;
    private void Start()
    {
        vec = transform.position;
    }

    public void MudaPosY(float pos)
    {
        vec.y = 250.8667f + pos; //ugh
        transform.SetPositionAndRotation(vec, Quaternion.identity);
    }
}
