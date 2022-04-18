using System;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace _Scripts.UI
{
    /// <summary>
    /// This class is for hold UI slots information like, sprite, text for display the amount of the item etc
    /// </summary>
    public class LabSlot : SlotBase
    {
        private void OnSlotClicked()
        {
            if (!LabEvents.IsMachineSlotSelected)
            {
                return;
            }
            LabEvents.IngredientSelected = uiSlot.item;
            //if (Slot.MachineSlot.amount >= Slot.maxPerSlot) return;
            if (LabEvents.IngredientSelected == null) return;
            if (LabEvents.MachineSlot.MachineSlot.item != null)
            {
                if (!LabEvents.IngredientSelected.Equals(LabEvents.MachineSlot.MachineSlot.item)) return;
            }
            if (!LabEvents.MachineSlot.itemRequired.HasFlag(LabEvents.IngredientSelected.ItemType))
            {
                return;
            }

            if (LabEvents.MachineSlot.MachineSlot.amount >= LabEvents.MachineSlot.maxPerSlot) return;
            LabEvents.OnIngredientSelectedCall();
            //LabEvents.OnItemRemoved(uiSlot.slotId, 1, InventoryType.Lab);
            _subject.StorageHolder.Storage.RemoveItem( uiSlot.slotId,1);
            _subject.UpdateInventory();
        }

        private void Start()
        {
            if (!TryGetComponent<Button>(out var button)) return;
            button.onClick.AddListener(OnSlotClicked);
        }
        
    }
}