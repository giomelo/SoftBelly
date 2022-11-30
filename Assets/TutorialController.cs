using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Singleton;
using _Scripts.SaveSystem;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private GameObject myFade;
    [SerializeField]
    private GameObject botao;
    [SerializeField]
    private GameObject skip;
    private Button butt;
    private Button skipButt;
    [SerializeField]
    private GameObject[] janelas;
    [SerializeField]
    private int tut = 0;

    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (!Savesystem.CheckIfSaveExists())
        {
            Invoke("StartTut", 1);
        }
    }

    public void StartGame()
    {
        myFade.SetActive(false);
        for (int i = 0; i < janelas.Length; i++)
            janelas[i].SetActive(false);
        botao.SetActive(false);
        skip.SetActive(false);
        GameManager.Instance.noRay = false;
        GameManager.Instance.noPause = false;
        Time.timeScale = 1;
    }

    private void StartTut()
    {
        GameManager.Instance.noRay = true;
        GameManager.Instance.noPause = true;
        myFade.SetActive(true);
        botao.SetActive(true);
        skip.SetActive(true);
        butt = botao.GetComponent<Button>();
        skipButt = skip.GetComponent<Button>();
        for (int i = 0; i < janelas.Length; i++)
            janelas[i].SetActive(true);
        Time.timeScale = 0;
    }

    public void PassaTutorial()
    {
        if (tut >= janelas.Length) { return; }
        butt.interactable = false;
        skipButt.interactable = false;
        StartCoroutine("WaitForFrames");
        anim.Play("Tut.Passa_" + tut);
        tut++;
    }

    public void DesativaJanela(int n)
    {
        janelas[n].SetActive(false);
    }

    private IEnumerator WaitForFrames()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        butt.interactable = true;
        skipButt.interactable = true;
    }
}
