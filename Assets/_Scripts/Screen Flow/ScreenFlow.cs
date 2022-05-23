using System.Collections;
using _Scripts.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.Screen_Flow
{
    public class ScreenFlow : MonoSingleton<ScreenFlow>
    {
        public GameObject LoadingScreen;
        public Slider Slider;
        public Text ProgressText;
        public GameObject OptionsScreen;

        #region Camera

        //public Transform Camera;
        //public Transform OptionsTarget;

        #endregion

        private void Start()
        {
            //DontDestroyOnLoad(this.gameObject);
        }

        public void LoadScene(string sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }

        IEnumerator LoadAsynchronously(string sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            LoadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                Slider.value = progress;
                ProgressText.text = progress * 100f + "%";
                yield return null;
            }
        }

        #region Options Screen

        public void OptionsScreenOn()
        {
            OptionsScreen.SetActive(true);
            //Camera.LookAt(OptionsTarget);
            
        }

        public void OptionsScreenOff()
        {
            OptionsScreen.SetActive(false);
        }

        #endregion
    

        public void BtnQuit()
        {
            Application.Quit();
        }
    }
}
