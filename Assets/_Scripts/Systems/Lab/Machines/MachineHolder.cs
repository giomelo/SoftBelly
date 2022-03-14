using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class MachineHolder : MonoBehaviour
    {
        public BaseMachine CurrentMachine;
        public void OnMachineSlotSelected(UIMachineSlot slot)
        {
            if (slot.Type != MachineSlotType.Ingredient) return;
            LabEvents.SlotSelected = slot;
        }
    }
}