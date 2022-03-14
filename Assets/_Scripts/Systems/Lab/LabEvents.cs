using System;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;

namespace _Scripts.Systems.Lab
{
    public static class LabEvents
    {
        public static Action<int> OnChestSelected;
        public static Action<BaseMachine> OnMachineSelected;
        public static Action<BaseMachine> OnMachineDispose;

        public static BaseMachine CurrentMachine;

        public static UIMachineSlot? SlotSelected = null;
        public static ItemBehaviour IngredientSelected;


        public static void OnChestSelectedCall(int id)
        {
            OnChestSelected?.Invoke(id);
        }
        
        public static void OnMachineSelectedCall(BaseMachine id)
        {
            CurrentMachine = id;
            OnMachineSelected?.Invoke(id);
        }
        
        public static void OnMachineDisposeCall(BaseMachine id)
        {
            
            OnMachineDispose?.Invoke(id);
        }

    }
}