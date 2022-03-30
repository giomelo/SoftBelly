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
                LabEvents.MachineSlot = Slot;
                LabEvents.IsMachineSlotSelected = true;
                HighLightSlot();
            }
            else
            {
                if (LabEvents.CurrentMachine == null ||
                    LabEvents.CurrentMachine.MachineState != MachineState.Ready) return;
                
                UnHighLight();
                LabEvents.CurrentMachine.uiController.UpdateInventory();
            }
           
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

            if (LabEvents.CurrentMachine.MachineState == MachineState.Working) return;
            if (Slot.MachineSlot.item == null) return;
            
            Debug.Log("Add");
            LabEvents.CurrentMachine.uiController.StorageHolder.Storage.AddItem(Slot.MachineSlot.amount,
                Slot.MachineSlot.item);
            Slot.MachineSlot.item = null;
            Slot.MachineSlot.amount = 0;

        }

        public void ResetSlot()
        {
            Slot.Image.color = Color.white;
            Slot.Image.sprite = null;
            Slot.Amount.text = "00";
            Slot.MachineSlot.item = null;
            Slot.MachineSlot.amount = 0;
        }
        
        //set the slot to the current item
        private void AddItemSlot(ItemBehaviour item)
        {
            if (LabEvents.MachineSlot.slotId != Slot.slotId) return;

            if (Slot.MachineSlot.amount >= Slot.maxPerSlot) return;
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