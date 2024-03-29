﻿using System;
using _Scripts.Helpers;
using _Scripts.Systems.Inventories;
using _Scripts.U_Variables;
using _Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;

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
            }
            else
                Debug.LogWarning("YOU CANT SLEEP RIGHT NOW");
            
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