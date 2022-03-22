
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Recipes;
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
        [SerializeField]
        private List<BaseMachineSlot> ResultsSlots = new List<BaseMachineSlot>();
        [SerializeField]
        private float _machineWorkingTime;
        
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
        public void StartMachine()
        {
            //if (machine.MachineId != MachineId) return;
            List<MachineSlot> ingredients = new List<MachineSlot>();
            foreach (var slotMachineObj in IngredientsSlots)
            {
                ingredients.Add(slotMachineObj.Slot.MachineSlot);
            }
            var auxRecipe = ScriptableObject.CreateInstance<RecipeObj>();
            auxRecipe.Init(ingredients);
            
            CurrentRecipe = AllRecipes.Instance.CheckRecipe(auxRecipe);
            
            if (CurrentRecipe == null) return;

            Debug.Log("Receita Existe");
            if (!LabTimeController.Instance.LabTimer.ContainsKey(MachineId))
            {
                LabTimeController.Instance.AddTime(MachineId, _machineWorkingTime, CurrentRecipe);
            }
            MachineState = MachineState.Working;
            uiController.DisposeInventory();
            StartCoroutine(LabTimeController.Instance.WorkMachine(this));
            LabEvents.OnMachineStartedCall(this);
        }

        public void SetState(MachineState state)
        {
            MachineState = state;
        }
        /// <summary>
        /// Create the ingredient result of the machine
        /// </summary>
        public void CreateResult()
        {
            for (int i = 0; i < ResultsSlots.Count; i++)
            {
                Debug.Log("Results");
                Debug.Log(ResultsSlots[i].Slot);
                Debug.Log(CurrentRecipe);
                ResultsSlots[i].Slot.Image.sprite = CurrentRecipe.Results[i].item.ImageDisplay;
                ResultsSlots[i].Slot.Amount.text = CurrentRecipe.Results[i].amount.ToString();
                ResultsSlots[i].Slot.MachineSlot.item = CurrentRecipe.Results[i].item;

            }
        }
    }
}
