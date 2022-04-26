using System;
using _Scripts.Helpers;
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
    }
}