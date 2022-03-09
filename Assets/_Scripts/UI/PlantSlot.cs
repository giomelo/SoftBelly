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
        /// When the user select a slot with a plant call the event OnPLantedSelected
        /// </summary>
        private void OnSlotClicked()
        {
            if (uiSlot.item == null) return;
            _subject.DisposeInventory();
            PlantEvents.CurrentPlant = (SeedBase)uiSlot.item;
            _subject.StorageHolder.Storage.RemoveItem( uiSlot.slotId,1);
            PlantEvents.OnPlantedSelected();
        }

        private void Start()
        {
            if (!TryGetComponent<Button>(out var button)) return;
            button.onClick.AddListener(OnSlotClicked);
        }
        
        /// <summary>
        /// On mouse enter, display current plant proprieties
        /// </summary>
        public void MouseEnter()
        {
            if (uiSlot.item == null)
            {
                _subject.ResetCurrentProprieties();
                return;
            }
            _subject.DisplayCurrentProprieties((SeedBase)uiSlot.item);
        }
        
        public void MouseExit()
        {
            _subject.ResetCurrentProprieties();
        }
    }
}
