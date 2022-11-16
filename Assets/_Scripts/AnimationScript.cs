using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField]  Animator aniamtor;
    void OnEnable()
    {
        aniamtor.SetBool("start", true);
    }
    void OnDisable()
    {
        aniamtor.SetBool("start", false);
    }
}
