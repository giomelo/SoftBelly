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
    /// <summary>
    /// Class for all machines
    /// </summary>
    [RequireComponent(typeof(MachineHolder))]
    public abstract class BaseMachine : MonoBehaviour
    {
        //public List<UIMachineSlot> Ingredients = new List<UIMachineSlot>();
        //public List<UIMachineSlot> Results  = new List<UIMachineSlot>();
        [SerializeField]
        private GameObject machineLayer;
        [SerializeField]
        public GameObject workingHUD;
        [Header("UIController of this machine")]
        public UIController uiController;
        public MachineState MachineState { get; set; } = MachineState.Empty;
        [SerializeField] public List<BaseMachineSlot> IngredientsSlots = new List<BaseMachineSlot>();
        [SerializeField] public List<BaseMachineSlot> ResultsSlots = new List<BaseMachineSlot>();
        [SerializeField]
        public float machineWorkingTime;

        public int MachineId;
        public RecipeObj CurrentRecipe;
        public bool IsDestroyed { get; private set; }
        public MachineHolder thisMachineHolder;

        public bool CanBurn;
        

        private void Start()
        {
            thisMachineHolder = gameObject.GetComponent<MachineHolder>();
        }
        private void OnEnable()
        {
            LabEvents.OnMachineSelected += OnCLicked;
            LabEvents.OnMachineDispose += OnDispose;
            LabEvents.OnMachineStarted += MachineProcess;
            IsDestroyed = false;
        }
        private void OnDisable()
        {
            LabEvents.OnMachineSelected -= OnCLicked;
            LabEvents.OnMachineDispose -= OnDispose;
            LabEvents.OnMachineStarted -= MachineProcess;
            IsDestroyed = true;
        }
        /// <summary>
        /// When machine is clicked
        /// </summary>
        /// <param name="id"></param>
        private void OnCLicked(BaseMachine id)
        {
            if (uiController.inventoryId != id.uiController.inventoryId) return;
            
            if (LabEvents.CurrentMachine as Pestle)
            {
                GameManager.Instance.camSwitcher.ChangeCamera();
            }
            machineLayer.SetActive(true);
            uiController.DisplayInventory(id.uiController.inventoryId);
        }
        
        /// <summary>
        /// Method for check all results in slot were collected, machines like the caldron only work if the player
        /// had collected all the remaining results from previous work
        /// </summary>
        /// <returns></returns>
        public bool CheckIfCollectedAllResults()
        {
            foreach (var slots in ResultsSlots)
            {
                if (slots.Slot.MachineSlot.item != null) return false;
            }
            return true;
        }
        /// <summary>
        /// Close Machine
        /// </summary>
        /// <param name="id"></param>
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
            if (LabEvents.CurrentMachine as Pestle)
            {
                GameManager.Instance.camSwitcher.ChangeCamera();
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
            StartTime();
            LabEvents.OnMachineStartedCall(this); //event for calling machine hud
        }
        
        protected void StartTime()
        { 
            if (!LabTimeController.Instance.LabTimer.ContainsKey(MachineId))
            {
                LabTimeController.Instance.AddTime(MachineId, machineWorkingTime, CurrentRecipe);
            }
            MachineState = MachineState.Working;
            uiController.DisposeInventory();
            StartCoroutine(LabTimeController.Instance.WorkMachine(thisMachineHolder));
        }
        
        /// <summary>
        /// ui machine process
        /// </summary>
        /// <param name="machine"></param>
        public void MachineProcess(BaseMachine machine)
        {
            if (machine.MachineId != MachineId) return;
            if (workingHUD)
            {
                GameObject HUD = GameObject.Instantiate(workingHUD, transform);
                MoveNeedle needle = HUD.GetComponentInChildren<MoveNeedle>();
                StartCoroutine(needle.StartNeedle(machine));
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
        
        /// <summary>
        /// Set ingredient slot for result slot, machines like herb dryer only have one slot for collection and putting
        /// </summary>
        public void SetSlotType()
        {
            foreach (var slot in IngredientsSlots)
            {
                if (slot.Slot.MachineSlot.item == null) continue;
                slot.Slot.Type = MachineSlotType.Result;
            }
        }
    }
}
