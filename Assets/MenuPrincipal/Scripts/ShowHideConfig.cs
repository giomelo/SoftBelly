using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideConfig : MonoBehaviour
{
    public int config = 0;
    public Animator anim;
    public AudioSource open;
    public AudioSource close;
    public ShowHideCreds cred;

    public void ShowHide()
    {
        StartCoroutine("SHConfig");
    }

    private IEnumerator SHConfig()
    {
        if (cred.credits == 1) { cred.ShowHide(); }
        switch (config)
        {
            case -1:
                break;

            case 0:
                config = -1;
                anim.Play("ConAnim.ShowConfig");
                bool dontOpen = false;
                if (cred.credits == 0) { cred.credits = -1; dontOpen = true; }
                open.Play();
                for (int i = 0; i < 55; i++) { yield return new WaitForFixedUpdate(); }
                config = 1;
                if (dontOpen) { cred.credits = 0; }
                break;

            case 1:
                config = -1;
                anim.Play("ConAnim.HideConfig");
                close.Play();
                for (int i = 0; i < 55; i++) { yield return new WaitForFixedUpdate(); }
                config = 0;
                break;
        }
    }
}
