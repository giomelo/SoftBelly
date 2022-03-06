using System;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    /// <summary>
    /// This class is for hold UI slots information like, sprite, text for display the amount of the item etc
    /// </summary>
    public class PlantSlot : MonoBehaviour
    {
        [SerializeField] 
        public UISlot uiSlot;
        
        
        /// <summary>
        /// When the user select a slot with a plant
        /// </summary>
        private void OnSlotClicked()
        {
            if (uiSlot.item == null) return;
            UIController.Instance.DisposeInventory();
            PlantEvents.CurrentPlant = (PlantBase)uiSlot.item;
            PlantEvents.OnPlantedSelected();
            Debug.Log("oi");
        }

        private void Start()
        {
            if (!TryGetComponent<Button>(out var button)) return;
            button.onClick.AddListener(OnSlotClicked);
        }
    }
}
