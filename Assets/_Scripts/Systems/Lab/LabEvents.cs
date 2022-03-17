using System;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;
using JetBrains.Annotations;

namespace _Scripts.Systems.Lab
{
    public static class LabEvents
    {
        public static Action<int> OnChestSelected;
        public static Action<BaseMachine> OnMachineSelected;
        public static Action<BaseMachine> OnMachineDispose;
        public static Action OnIngredientSelected;

        [CanBeNull] public static BaseMachine CurrentMachine = null;

        public static UIMachineSlot MachineSlot;
        public static ItemBehaviour IngredientSelected;
        public static bool IsMachineSlotSelected; 

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
        
        public static void OnIngredientSelectedCall()
        {
            MachineSlot.MachineSlot.item= IngredientSelected;
            OnIngredientSelected?.Invoke();
        }

    }
}