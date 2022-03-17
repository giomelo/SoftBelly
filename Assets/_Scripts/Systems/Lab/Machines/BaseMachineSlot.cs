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
        }
    }
}