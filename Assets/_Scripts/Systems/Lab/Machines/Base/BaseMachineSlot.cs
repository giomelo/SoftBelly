using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.Base
{
    public class BaseMachineSlot : MonoBehaviour
    {
        public UIMachineSlot Slot;
        
        public void OnMachineSlotSelected()
        {
            if (Slot.Type == MachineSlotType.Ingredient)
            {
                if (LabEvents.IsMachineSlotSelected)
                {
                    UnHighLightSlot(LabEvents.MachineSlot);
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
            Slot.Image.color = Color.green;
        }

        private static void UnHighLightSlot(UIMachineSlot slot)
        {
            slot.Image.color = Color.white;
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
            
            Slot.Image.color = Color.white;
            Slot.Image.sprite = null;
            Slot.Amount.text = "00";

            if (LabEvents.CurrentMachine.MachineState == MachineState.Working) return;
            if (Slot.MachineSlot.item == null) return;
            
            LabEvents.CurrentMachine.uiController.StorageHolder.Storage.AddItem(Slot.MachineSlot.amount,
                Slot.MachineSlot.item);
            LabEvents.CurrentMachine.uiController.UpdateInventory();
            Debug.LogWarning("Item");
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
                            }
                        }
                        break;
                    case 1:
                        if (currentMachine == currentMachine as HerbDryer)
                        {
                            LabEvents.CurrentMachine.SetState(MachineState.Empty);
                            SetType(MachineSlotType.Ingredient);
                        }
                        break;
                }
            }
         
          
        }

     
        public static void ResetSlot(UIMachineSlot slot)
        {
            slot.Image.color = Color.white;
            slot.Image.sprite = null;
            slot.Amount.text = "00";
            slot.MachineSlot.item = null;
            slot.MachineSlot.amount = 0;
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