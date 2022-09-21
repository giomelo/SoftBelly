using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCreds : MonoBehaviour
{
    public ShowHideConfig conf;
    public int credits = 0;
    public Animator anim;

    public void ShowHide()
    {
        StartCoroutine("SHCreds");
    }

    private IEnumerator SHCreds()
    {
        if (conf.config == 1) { conf.ShowHide(); }
        switch (credits)
        {
            case -1:
                break;

            case 0:
                credits = -1;
                bool dontOpen = false;
                if (conf.config == 0) { conf.config = -1; dontOpen = true; }
                anim.Play("CredAnim.ShowCredits");
                for (int i = 0; i < 55; i++) { yield return new WaitForFixedUpdate(); }
                credits = 1;
                if (dontOpen) { conf.config = 0; }
                break;

            case 1:
                credits = -1;
                anim.Play("CredAnim.HideCredits");
                for (int i = 0; i < 55; i++) { yield return new WaitForFixedUpdate(); }
                credits = 0;
                break;
        }
    }

}
