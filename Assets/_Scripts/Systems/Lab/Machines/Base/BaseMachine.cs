using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Lab.Recipes;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.Base
{
    public abstract class BaseMachine : MonoBehaviour
    {
        //public List<UIMachineSlot> Ingredients = new List<UIMachineSlot>();
        //public List<UIMachineSlot> Results  = new List<UIMachineSlot>();
        [SerializeField]
        private GameObject machineLayer;
        [SerializeField]
        private GameObject workingHUD;
        [Header("UIController of this machine")]
        public UIController uiController;
        public MachineState MachineState { get; set; } = MachineState.Empty;
        [SerializeField] public List<BaseMachineSlot> IngredientsSlots = new List<BaseMachineSlot>();
        [SerializeField] public List<BaseMachineSlot> ResultsSlots = new List<BaseMachineSlot>();
        [SerializeField]
        private float machineWorkingTime;
        
        [SerializeField]
        private float machineBurnTime;
        
        public int MachineId;
        public RecipeObj CurrentRecipe;
        public bool IsDestroyed { get; private set; }
        private void OnEnable()
        {
            LabEvents.OnMachineSelected += OnCLicked;
            LabEvents.OnMachineDispose += OnDispose;
            IsDestroyed = false;
        }
        private void OnDisable()
        {
            LabEvents.OnMachineSelected -= OnCLicked;
            LabEvents.OnMachineDispose -= OnDispose;
            IsDestroyed = true;
        }

        private void OnCLicked(BaseMachine id)
        {
            if (uiController.inventoryId != id.uiController.inventoryId) return;
            machineLayer.SetActive(true);
            uiController.DisplayInventory(id.uiController.inventoryId);
        }

        public bool CheckIfCollectedAllResults()
        {
            foreach (var slots in ResultsSlots)
            {
                if (slots.Slot.MachineSlot.item != null) return false;
            }
            return true;
        }
        private void OnDispose(BaseMachine id)
        {
            if (LabEvents.CurrentMachine == null) return;
            if (uiController.inventoryId != id.uiController.inventoryId) return;
            machineLayer.SetActive(false);
            LabEvents.IsMachineSlotSelected = false;
            foreach (var u in IngredientsSlots)
            {
                u.UnHighLight();
            }
            LabEvents.CurrentMachine = null;
            
        }
        /// <summary>
        /// Button for machines that have timer
        /// </summary>
        protected virtual void StartMachine()
        {
            //if (machine.MachineId != MachineId) return;
            List<ItemObj> ingredients = new List<ItemObj>();
            foreach (var slotMachineObj in IngredientsSlots)
            {
                ingredients.Add(slotMachineObj.Slot.MachineSlot);
            }
            var auxRecipe = ScriptableObject.CreateInstance<RecipeObj>();
            auxRecipe.Init(ingredients);
            
            CurrentRecipe = AllRecipes.Instance.CheckRecipe(auxRecipe);
            
            if (CurrentRecipe == null) return;

            Debug.Log("Receita Existe");
           
            StartTime();
            
            LabEvents.OnMachineStartedCall(this); //event for calling machine hud
            
            MachineProcess(machineWorkingTime);
        }

        protected void StartTime()
        { 
            if (!LabTimeController.Instance.LabTimer.ContainsKey(MachineId))
            {
                LabTimeController.Instance.AddTime(MachineId, machineWorkingTime, CurrentRecipe);
            }
            MachineState = MachineState.Working;
            uiController.DisposeInventory();
            StartCoroutine(LabTimeController.Instance.WorkMachine(this));
        }

        private void MachineProcess(float machineWorkingTime)
        {
            if (workingHUD)
            {
                GameObject HUD = GameObject.Instantiate(workingHUD, transform);
                MoveNeedle needle = HUD.GetComponentInChildren<MoveNeedle>();
                needle.needleTime = machineWorkingTime;
            }
                
        }

        public void SetState(MachineState state)
        {
            MachineState = state;
        }

        /// <summary>
        /// Create the ingredient result of the machine
        /// </summary>
        public abstract void CreateResult();
        
        public void SetSlotType()
        {
            Debug.LogWarning("SLOTTYPE CHANGED"); 
            foreach (var slot in IngredientsSlots)
            {
                if (slot.Slot.MachineSlot.item == null) continue;
                slot.Slot.Type = MachineSlotType.Result;
            }
        }
    }
}
