using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using _Scripts.Enums;
using Unity.VisualScripting;

namespace _Scripts.Singleton
{
    public class PlantTimeController : MonoSingleton<PlantTimeController>
    {
        public Dictionary<int, float> PlantTimer = new Dictionary<int,float>();

        public void AddTime(int plotId, float time)
        {
            PlantTimer.Add(plotId, time);
        }

        private void ReduceTime(int key)
        {
            
        }


        public void ClearSlot(int id)
        {
            PlantTimer.Remove(id);
        }
        
    }
}