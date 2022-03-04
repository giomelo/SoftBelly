using System;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Class responsible for call the event when the player plants
    /// </summary>
    public static class PlantEvents
    {
        public static Action<int> OnPlanted;

        public static void OnPlantedCall(int id)
        {
            //this is the same as doing: if(Onplanted != null)
            OnPlanted?.Invoke(id);
        }
    }
}
