using _Scripts.Enums;
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
            if (LabEvents.CurrentMachine.MachineState == MachineState.Working) return;
            if (Slot.Type == MachineSlotType.Ingredient)
            {
                if (LabEvents.IsMachineSlotSelected)
                {
                    if (LabEvents.MachineSlot.MachineSlot.item == null)
                    {
                        UnHighLightSlot(LabEvents.MachineSlot);
                    }
                    else
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
                // if (LabEvents.CurrentMachine == null ||
                //     LabEvents.CurrentMachine.MachineState != MachineState.Ready) return;

                if (LabEvents.CurrentMachine == null || Slot.MachineSlot.item == null) return;
                
                UnHighLight();
                
            }
           
        }

        private void HighLightSlot()
        {
            Slot.HighImage.color = Color.green;
        }

        private static void UnHighLightSlot(UIMachineSlot slot)
        {
            slot.HighImage.color = Color.black;
            slot.Image.sprite = null;
            slot.Amount.text = "00";
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
            
            LabEvents.CurrentMachine.uiController.StorageHolder.Storage.AddItem(Slot.MachineSlot.amount,
                Slot.MachineSlot.item);
            LabEvents.CurrentMachine.uiController.UpdateInventory();
            Slot.MachineSlot.item = null;
            Slot.MachineSlot.amount = 0;

            var currentMachine = LabEvents.CurrentMachine;
            for (int i = 0; i < 2; i++)
            {
                switch (i)
                {
                    case 0:
                        if (currentMachine == currentMachine as Cauldron)
                        {
                            if (LabEvents.CurrentMachine.CheckIfCollectedAllResults())
                            {
                                LabEvents.CurrentMachine.SetState(MachineState.Empty);
                                LabTimeController.Instance.LabTimer.Remove(currentMachine.MachineId);
                            }
                        }
                        break;
                    case 1:
                        if (currentMachine == currentMachine as HerbDryer)
                        {
                            var herbDryer = currentMachine as HerbDryer;
                            LabEvents.CurrentMachine.SetState(MachineState.Empty);
                          
                            SetType(MachineSlotType.Ingredient);
                            herbDryer.RemovePlantObject(Slot.slotId);
                        }
                        break;
                }
            }
        }
        
     
        public void ResetSlot()
        {
            UnHighLightSlot(Slot);
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
            LabEvents.MachineSlot = Slot;
        }

        private void RemoveItemSlot()
        {
            Slot.MachineSlot.amount--;
            Slot.Amount.text = Slot.MachineSlot.amount.ToString();
            LabEvents.CurrentMachine.uiController.StorageHolder.Storage.AddItem(1, Slot.MachineSlot.item);
            LabEvents.CurrentMachine.uiController.UpdateInventory();
            if (Slot.MachineSlot.amount <= 0)
            {
                ResetSlot();
            }
        }

        private void SetType(MachineSlotType type)
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