using System;
using _Scripts.Helpers;
using _Scripts.U_Variables;
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