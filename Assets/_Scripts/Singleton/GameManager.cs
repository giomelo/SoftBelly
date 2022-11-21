using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using _Scripts.SaveSystem;
using _Scripts.Systems.Inventories;
using _Scripts.U_Variables;
using _Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Singleton
{
    /// <summary>
    /// Manager for store basic scripts and camera
    /// </summary>
    public class GameManager : MonoSingleton<GameManager>
    {
        public Camera MainCamera;
        public CineMachineSwitcher camSwitcher;
        public Transform Player;

        public static Action Sleep;
        private bool sleeping;

        public static Action PromotionLevel;

        public StorageHolder plantStorage;
        public StorageHolder labStorage;

        public bool noRay = false;
        public bool noPause = false;
        
        public static bool hasInventory = false;
        public UIController currentUi;
        public void SleepCall()
        {
            if (DaysController.Instance.time.Hours >= DaysController.Instance.finisHourPatients)
            {
                if (sleeping) return;
                
                Sleep?.Invoke();
                sleeping = true;
                Save();
            }
            else
            {
                HUD_Controller.Instance.AvisoCama();
                Debug.LogWarning("YOU CANT SLEEP RIGHT NOW");
            }
               
            
        }
        
        public void Save()
        {
            var saveabless = new List<DataObject>();
            for(var i = 0; i < SceneManager.sceneCount; i++)
            {
                var rootObjs = SceneManager.GetSceneAt(i).GetRootGameObjects();
                foreach(var root in rootObjs)
                {
                    saveabless.AddRange(root.GetComponentsInChildren<DataObject>(true));
                }
            }
            foreach (var i in saveabless)
            {
                i.Save();
            }
        }
        
        public void PromotionLevelCall()
        {
            PromotionLevel?.Invoke();
        }
        private void UpdateGameState()
        {
           
        }
    }
}