﻿using System;
using _Scripts.Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Singleton
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Camera MainCamera;
        public CineMachineSwicher CamSwicher;
    }
}