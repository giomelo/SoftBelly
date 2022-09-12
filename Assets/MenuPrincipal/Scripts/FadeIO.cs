using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIO : MonoBehaviour
{
    public Image fade;
    public float FADESPEED;

    void Start()
    {
        StartFade();
    }

    public void StartFade()         //Porque botões não gostam de iniciar corotinas, então é melhor deixar uma função aqui
    {
        StartCoroutine("Fade");
    }

    public IEnumerator Fade()
    {
        fade.raycastTarget = !fade.raycastTarget;
        float inOut = fade.color.a > 0 ? -FADESPEED : FADESPEED;
        Color alpha = new Color(0, 0, 0, inOut * Time.fixedDeltaTime);
        for (int i = 0; i < (1 / Time.fixedDeltaTime) / FADESPEED; i++)
        {
            fade.color += alpha;
            yield return new WaitForFixedUpdate();
        }
    }
}
