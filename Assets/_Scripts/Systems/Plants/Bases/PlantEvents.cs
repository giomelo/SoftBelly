using System;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plantation;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Class responsible for call the event when the player plants
    /// </summary>
    public static class PlantEvents
    {
        public static Action<Plot> OnPlanted;
        public static Action OnPlotSelected;

        public static PlantBase CurrentPlant;
        public static Plot currentPlot;
        
        public static void OnPlantedCall(Plot id)
        {
            //this is the same as doing: if(Onplanted != null)
            OnPlanted?.Invoke(id);
        }

        public static void OnPlotSelectedCall()
        {
            OnPlotSelected?.Invoke();
        }

        public static void OnPlantedSelected()
        {
            Debug.Log("retirou");
            UIController.Instance.storageHolder.Storage.RemoveItem(CurrentPlant, 1);
            OnPlantedCall(currentPlot);
        }
    }
}
