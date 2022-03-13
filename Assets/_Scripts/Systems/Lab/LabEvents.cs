using System;

namespace _Scripts.Systems.Lab
{
    public static class LabEvents
    {
        public static Action<int> OnChestSelected;

        public static void OnChestSelectedCall(int id)
        {
            OnChestSelected?.Invoke(id);
        }
    }
}