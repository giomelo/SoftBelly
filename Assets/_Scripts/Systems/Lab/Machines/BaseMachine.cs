
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public abstract class BaseMachine : MonoBehaviour
    {
        //public List<UIMachineSlot> Ingredients = new List<UIMachineSlot>();
        //public List<UIMachineSlot> Results  = new List<UIMachineSlot>();
        [SerializeField]
        private GameObject machineLayer;
        [Header("UIController of this machine")]
        public UIController uiController;
        public MachineState MachineState { get; set; } = MachineState.Empty;
        [SerializeField]
        private List<BaseMachineSlot> IngredientsSlots = new List<BaseMachineSlot>();

        private void OnEnable()
        {
            LabEvents.OnMachineSelected += OnCLicked;
            LabEvents.OnMachineDispose += OnDispose;
        }
        private void OnDisable()
        {
            LabEvents.OnMachineSelected -= OnCLicked;
            LabEvents.OnMachineDispose -= OnDispose;
        }

        private void OnCLicked(BaseMachine id)
        {
            if (uiController.inventoryId != id.uiController.inventoryId) return;
            machineLayer.SetActive(true);
            uiController.DisplayInventory(id.uiController.inventoryId);
        }

        private void OnDispose(BaseMachine id)
        {
            if (LabEvents.CurrentMachine == null) return;
            if (uiController.inventoryId != id.uiController.inventoryId) return;
            machineLayer.SetActive(false);
            LabEvents.IsMachineSlotSelected = false;
            foreach (BaseMachineSlot u in IngredientsSlots)
            {
                u.UnHighLight();
            }
            LabEvents.CurrentMachine = null;
            
        }

    }
}
