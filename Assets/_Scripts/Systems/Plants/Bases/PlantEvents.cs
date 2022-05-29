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
        public static Action<Plot> OnHarvest;
        public static Action<int> LabInventoryAction;
        public static Action<int> OnPlotSelected;

        public static SeedBase CurrentPlant;
        public static Plot CurrentPlot;
        public static PlantBase PlantCollected;

        private static void OnPlantedCall(Plot id)
        {
            //this is the same as doing: if(Onplanted != null)
            OnPlanted?.Invoke(id);
        }

        public static void OnPlotSelectedCall(int id)
        {
            OnPlotSelected?.Invoke(id);
        }

        public static void OnHarvestCall(Plot id)
        {
            OnHarvest?.Invoke(id);
        }

        public static void OnLabInventoryAction(int id)
        {
            Debug.Log("lAbinventoryaction");
            LabInventoryAction?.Invoke(id);
        }
        public static void OnPlantedSelected()
        {
            OnPlantedCall(CurrentPlot);
        }
    }
}
