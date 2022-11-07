using System.Collections;
using System.Linq;
using _Scripts.SaveSystem;
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
        public GameObject credits;
        public GameObject telaInicial;

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

            if (operation.isDone)
            {
                var objs = FindObjectsOfType<MonoBehaviour>().OfType<DataObject>();
                foreach (var o in objs)
                {
                    Debug.Log("Load");
                    o.Load();
                }
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

        public void Oncredidts()
        {
            credits.SetActive(true);
            telaInicial.SetActive(false);
        }
        public void OUTcredidts()
        {
            credits.SetActive(false);
            telaInicial.SetActive(true);
        }
        public void BtnQuit()
        {
            Application.Quit();
        }
    }
}
