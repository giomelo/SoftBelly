using System;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    /// <summary>
    /// This class is for hold UI slots information like, sprite, text for display the amount of the item etc
    /// </summary>
    public class Slot : MonoBehaviour
    {
        [SerializeField] 
        public UISlot uiSlot;
        
        
        /// <summary>
        /// When the user select a slot with a plant
        /// </summary>
        private void OnSlotClicked()
        {
            UIController.Instance.DisposeInventory();
            PlantEvents.CurrentPlant = (PlantBase)uiSlot.item;
            PlantEvents.OnPlantedSelected();
        }

        private void OnEnable()
        {
            if (!TryGetComponent<Button>(out var button)) return;
            button.onClick.AddListener(OnSlotClicked);
        }
    }
}
