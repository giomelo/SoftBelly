using System;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace _Scripts.UI
{
    /// <summary>
    /// This class is for hold UI slots information like, sprite, text for display the amount of the item etc
    /// </summary>
    public class PlantSlot : SlotBase
    {

        /// <summary>
        /// When the user select a slot with a plant
        /// </summary>
        private void OnSlotClicked()
        {
            if (uiSlot.item == null) return;
            _subject.DisposeInventory();
            PlantEvents.CurrentPlant = (PlantBase)uiSlot.item;
            _subject.storageHolder.Storage.RemoveItem(PlantEvents.CurrentPlant, 1);
            PlantEvents.OnPlantedSelected();
        }

        private void Start()
        {
            if (!TryGetComponent<Button>(out var button)) return;
            button.onClick.AddListener(OnSlotClicked);
        }

        public void MouseEnter()
        {
            Debug.Log("mouseEnter");
            if (uiSlot.item == null) return;
            _subject.DisplayCurrentProprieties((PlantBase)uiSlot.item);
        }
    }
}
