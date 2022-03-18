﻿using System;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class BaseMachineSlot : MonoBehaviour
    {
        public UIMachineSlot Slot;
        
        public void OnMachineSlotSelected()
        {
            if (Slot.Type != MachineSlotType.Ingredient) return;
            LabEvents.MachineSlot = Slot;
            LabEvents.IsMachineSlotSelected = true;
            HighLightSlot();
        }

        private void HighLightSlot()
        {
            Slot.Image.color = Color.green;
        }

        public void UnHighLight()
        {
            Slot.Image.color = Color.white;
            Slot.Image.sprite = null;
            Slot.Amount.text = "00";
         
                if (LabEvents.CurrentMachine == null)
                {
                    Debug.LogError("Missing UiController of this machine");
                    return;
                }
                LabEvents.CurrentMachine.uiController.StorageHolder.Storage.AddItem(Slot.MachineSlot.amount,
                        Slot.MachineSlot.item);
           
            Slot.MachineSlot.item = null;
            Slot.MachineSlot.amount = 0;   

        }
        
        //set the slot to the current item
        private void AddItemSlot(ItemBehaviour item)
        {
            if (LabEvents.MachineSlot.slotId != Slot.slotId) return;
            Slot.MachineSlot.item = item;
            Slot.MachineSlot.amount++;
            Slot.Amount.text = Slot.MachineSlot.amount.ToString();
            Slot.Image.sprite = item.ImageDisplay;
        }

        private void OnEnable()
        {
            LabEvents.OnIngredientSelected += AddItemSlot;
        }

        private void OnDisable()
        {
            LabEvents.OnIngredientSelected -= AddItemSlot;
        }
    }
}