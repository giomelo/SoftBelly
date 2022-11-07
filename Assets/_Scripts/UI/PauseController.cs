using System;
using System.Collections;
using System.Linq;
using _Scripts.SaveSystem;
using _Scripts.Screen_Flow;
using UnityEngine;
using _Scripts.Singleton;

namespace _Scripts.UI
{
    [Serializable]
    public enum PauseButton
    {
        Resume,
        Options,
        Controlls,
        MainMenu,
        Exit,
        Back
    }
    public class PauseController : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseObj;
        [SerializeField]
        private GameObject controllsObj;
        [SerializeField]
        private GameObject optionsObj;
        private GameObject currentMenu;

        //Mudan�as de nova UI
        [SerializeField]
        private int menuState = 0;
        [SerializeField]
        private int satelliteMenuState = 0;
        [SerializeField]
        private Animator anim;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (GameManager.hasInventory)
                {
                    GameManager.Instance.currentUi.DisposeInventory();
                    return;
                }
                    
                if(menuState != -1 && !GameManager.Instance.noPause)
                    PauseInput();
            }
        }

        private void PauseInput()
        {
            StartCoroutine("SHMenu");
        }

        public void PauseButton(ButtonsComponents button)
        {
            
            switch (button.Button)
            {
                case UI.PauseButton.Resume:
                    PauseInput();
                    break;
                case UI.PauseButton.Options:    //Ambos executam o mesmo c�digo com poucas altera��es
                case UI.PauseButton.Controlls:
                    if (menuState == 1)
                    {
                        string[] anims = new string[3];
                        if (button.Button == UI.PauseButton.Options) { anims = new string[3] { "PauseAnim.ShowOptions", "PauseAnim.HideOptions", "PauseAnim.HideContShowOp" }; }
                        if (button.Button == UI.PauseButton.Controlls) { anims = new string[3] { "PauseAnim.ShowControl", "PauseAnim.HideControl", "PauseAnim.HideOpShowCont" }; }
                        int am = button.Button == UI.PauseButton.Options ? 2 : 1;
                        int other = button.Button == UI.PauseButton.Options ? 1 : 2;
                        menuState = 2;
                        PauseInput();
                        if (satelliteMenuState == 0) {
                            anim.Play(anims[0]);
                            satelliteMenuState = am;
                        }
                        else if (satelliteMenuState == am) { 
                            anim.Play(anims[1]);
                            satelliteMenuState = 0;
                        }
                        else if (satelliteMenuState == other) { 
                            anim.Play(anims[2]);
                            satelliteMenuState = am;
                        }
                    }
                    break;
                case UI.PauseButton.MainMenu:
                    pauseObj.SetActive(false);
                    Time.timeScale = 1;
                   
                    var objs = FindObjectsOfType<MonoBehaviour>().OfType<DataObject>();
                    foreach (var o in objs)
                    {
                        o.Save();
                    }
                    ScreenFlow.Instance.LoadScene("NovoMenu");
                    break;
                case UI.PauseButton.Exit:
                    Time.timeScale = 1;
                    
                    var objss = FindObjectsOfType<MonoBehaviour>().OfType<DataObject>();
                    foreach (var o in objss)
                    {
                        o.Save();
                    }
                    Application.Quit();
                    break;
            }
        }

        private IEnumerator SHMenu()
        {
            float startTime = Time.realtimeSinceStartup;    //realtimeSinceStartup � usado aqui pois os yield waitfors das corotinas n�o funcionam com timescale 0
            switch (menuState)
            {
                case -1:
                    break;

                case 0:
                    menuState = -1;
                    pauseObj.SetActive(!pauseObj.activeInHierarchy);
                    Time.timeScale = 0;
                    anim.Play("PauseAnim.ShowPause");
                    while (Time.realtimeSinceStartup - startTime < 1.1f) { yield return null; }
                    menuState = 1;
                    break;

                case 1:
                    menuState = -1;
                    if (satelliteMenuState == 1)
                        anim.Play("PauseAnim.HidePauseSatCont");
                    else if (satelliteMenuState == 2)
                        anim.Play("PauseAnim.HidePauseSatOpt");
                    else
                        anim.Play("PauseAnim.HidePause");
                    while (Time.realtimeSinceStartup - startTime < 1.1f) { yield return null; }
                    satelliteMenuState = 0;
                    menuState = 0;
                    pauseObj.SetActive(!pauseObj.activeInHierarchy);
                    Time.timeScale = 1;
                    break;

                case 2:
                    menuState = -1;
                    while (Time.realtimeSinceStartup - startTime < 1.1f) { yield return null; }
                    menuState = 1;
                    break;
            }
        }
    }
}
