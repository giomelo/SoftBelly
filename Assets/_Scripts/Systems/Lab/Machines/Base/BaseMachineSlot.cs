﻿using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.Base
{
    /// <summary>
    /// Class for slots in machine
    /// </summary>
    public class BaseMachineSlot : MonoBehaviour
    {
        public UIMachineSlot Slot;

        public void OnMachineSlotSelected()
        {
            if (LabEvents.CurrentMachine != null && LabEvents.CurrentMachine.MachineState == MachineState.Working) return;
            if (Slot.Type == MachineSlotType.Ingredient)
            {
                if (LabEvents.IsMachineSlotSelected)
                {
                    UnHighLightSlot(LabEvents.MachineSlot);
                    if (LabEvents.MachineSlot.MachineSlot.item != null)
                    {
                        if (LabEvents.MachineSlot.Equals(Slot))
                        {
                            RemoveItemSlot();
                          
                        }
                    }
                }
                LabEvents.IsMachineSlotSelected = true;
                LabEvents.MachineSlot = Slot;
                HighLightSlot();
            }
            else
            {
                if (LabEvents.CurrentMachine == null || Slot.MachineSlot.item == null) return;
                if (LabEvents.CurrentMachine.MachineState == MachineState.Ready)
                {
                    //RemoveItemSlot();
                    UnHighLight();
                }
                // else
                // {
                //     UnHighLight();
                // }

            }

        }

        private void HighLightSlot()
        {
            Slot.HighImage.color = Color.green;
        }

        private void UnHighLightSlot(UIMachineSlot slot)
        {
            slot.HighImage.color = Color.black;
            //slot.Image.sprite = null;
            //slot.Amount.text = "00";
        }
        public void UnHighLight()
        {
            if (LabEvents.CurrentMachine == null)
            {
                Debug.LogError("Missing UiController of this machine");
                return;
            }

            UnHighLightSlot(Slot);

            if (LabEvents.CurrentMachine.MachineState == MachineState.Working) return;
            if (Slot.MachineSlot.item == null) return;
            RemoveItemSlot();

            LabEvents.CurrentMachine.CheckFinishMachine(this);
        }


        public void ResetSlot()
        {
            UnHighLightSlot(Slot);
            Slot.MachineSlot.item = null;
            Slot.MachineSlot.amount = 0;
            Slot.Image.sprite = null;
            Slot.Amount.text = "00";
        }

        //set the slot to the current item
        private void AddItemSlot(ItemBehaviour item)
        {
            if (LabEvents.MachineSlot.slotId != Slot.slotId) return;
            Slot.MachineSlot.item = item;
            Slot.MachineSlot.amount++;
            Slot.Amount.text = Slot.MachineSlot.amount.ToString();
            Slot.Image.sprite = item.ImageDisplay;
            LabEvents.MachineSlot = Slot;
        }

        private void RemoveItemSlot()
        {
            Slot.MachineSlot.amount--;
            Slot.Amount.text = Slot.MachineSlot.amount.ToString();
            LabEvents.CurrentMachine.uiController.StorageHolder.Storage.AddItem(1, Slot.MachineSlot.item);
            LabEvents.CurrentMachine.uiController.UpdateInventory();
            LabEvents.CurrentMachine.uiController.ShowItemsAvailable(LabEvents.CurrentMachine);
            if (Slot.MachineSlot.amount <= 0)
            {
                ResetSlot();
            }
        }

        public void SetType(MachineSlotType type)
        {
            Slot.Type = type;
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