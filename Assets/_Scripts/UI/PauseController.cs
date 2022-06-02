using System;
using _Scripts.Screen_Flow;
using UnityEngine;

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

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        
        }
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            PauseInput();
        }

        private void PauseInput()
        {
            pauseObj.SetActive(!pauseObj.activeInHierarchy);
            Time.timeScale = pauseObj.activeInHierarchy ? 0 : 1;
        }
        
        public void PauseButton(ButtonsComponents button)
        {
            
            switch (button.Button)
            {
                
                case UI.PauseButton.Controlls:
                    Debug.Log("Contorle");
                    controllsObj.SetActive(true);
                    currentMenu = controllsObj;
                    break;
                case UI.PauseButton.Resume:
                    pauseObj.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case UI.PauseButton.Options:
                    // optionsObj.SetActive(true);
                    // currentMenu = optionsObj;
                    break;
                case UI.PauseButton.Back:
                    currentMenu.SetActive(false);
                    break;
                case UI.PauseButton.MainMenu:
                    pauseObj.SetActive(false);
                    Time.timeScale = 1;
                    ScreenFlow.Instance.LoadScene("Menu");
                    break;
                case UI.PauseButton.Exit:
                    Time.timeScale = 1;
                    Application.Quit();
                    break;
            }
        }
    }
}
