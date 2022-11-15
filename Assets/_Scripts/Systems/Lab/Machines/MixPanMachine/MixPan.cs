using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using _Scripts.Systems.Plants.Bases;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using ProgressBar = _Scripts.Helpers.ProgressBar;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    public class MixPan : BaseMachine, IMix
    {
        [SerializeField]
        private List<DragObject> decorators = new List<DragObject>();
        [SerializeField]
        private SpoonObj spoonObj; 
        [SerializeField]
        private GameObject ingredietnsShelf;
        [SerializeField]
        private Transform pos;
        
        [SerializeField]
        private TextMeshProUGUI decoratorsText;
        [SerializeField]
        private Button resetButton;

        public static Action<IngredientToppingSO> OnIngredientAdd;
        private Dictionary<IngredientToppingSO, int> decoratorsAdded = new Dictionary<IngredientToppingSO, int>(); //lista para adicionar todos os ingredientes
        [SerializeField]
        private ProgressBar progressBar;

        private float _coolDown = 0.3f;
        private int _currentHits;
        private int _hitsNecessaries = 30;

        public bool CanHit { get; set; }
        public bool Holding { get; set; }

        public override void Start()
        {
            base.Start();
            progressBar.Maximum = _hitsNecessaries;
            CanHit = true;
            resetButton.onClick.AddListener(Reset);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            OnIngredientAdd += AddText;
            LabEvents.OnItemMixed += AddHits;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            OnIngredientAdd -= AddText;
            LabEvents.OnItemMixed -= AddHits;
        }

        private void AddText(IngredientToppingSO ingredient)
        {
            
            if (decoratorsAdded.ContainsKey(ingredient))
                decoratorsAdded[ingredient] += 1;
            else
                decoratorsAdded.Add(ingredient, 1);
            SetText();
                
        }

        private void ResetText()
        {
            decoratorsText.text = "";
            decoratorsAdded.Clear();
        }
        public static void OnIngredientAddCall(IngredientToppingSO i)
        {
            OnIngredientAdd?.Invoke(i);
        }
        public override void CreateResult()
        {
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item == null) continue;
                
                var newMixedPlant = ScriptableObject.CreateInstance<BaseMixedItem>();
                
                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                newMixedPlant.name = currentPlant.ItemId + "Mixed " + decoratorsText.text;

                newMixedPlant.Init(newMixedPlant.name, ItemType.MixedPlant, currentPlant.MixedPlant,currentPlant.Price, currentPlant.ItemProprieties.ItemProprietiesGO, newMixedPlant.name, GenerateToppingList(), currentPlant);
                IngredientsSlots[i].Slot.Image.sprite = newMixedPlant.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = newMixedPlant;
                IngredientsSlots[i].Slot.Amount.text = 1.ToString();
                IngredientsSlots[i].Slot.MachineSlot.amount = 1;
                
                newMixedPlant.BasePlant.AddMachine(MachineTypes);
            }

            if(MachineState == MachineState.Ready)
            {
                SetSlotType();
            }

            RestartMachine();
            LabEvents.OnMachineFinishedCall(this);
        }

        private void RestartMachine()
        {
           DeleteObject();
           ResetText();
           ResetBar();
           _currentHits = 0;
        }

        private void Reset()
        {
            ResetText();
            ResetBar();
            _currentHits = 0;
        }

        private void DeleteObject()
        {
            if (pos.childCount <= 0) return;
            for (int i = 0; i < pos.childCount; i++)
            {
                Destroy(pos.GetChild(i).gameObject);
            }
        }
        private void ResetBar()
        {
            progressBar.SetCurrentValue(0);
        }

        private void SetText()
        {
            decoratorsText.text = "";
            foreach (KeyValuePair<IngredientToppingSO, int> key in decoratorsAdded)
            {
                decoratorsText.text += " - " + key.Value + "x" + " " + key.Key.IngredientDescription;
            }
        }
        
        protected override void InitMachine()
        {
            ingredietnsShelf.SetActive(true);
        }
        protected override void FinishMachine()
        {
         
            OnDisposeMachine();
        }

        private void OnDisposeMachine()
        {
            ingredietnsShelf.SetActive(false);
            RestartMachine();
            MachineState = MachineState.Empty;
        }

        public void Work()
        {
            if (MachineState is MachineState.Working or MachineState.Ready) return;
            
            // drag 3d
            // foreach (var ingredient in decorators)
            // {
            //     ingredient.StartDrag();
            // }
            // spoonObj.StartDrag();
            SetState(MachineState.Working);
            //Create();
        }

        public void AddButton(IngredientToppingSO ingredient)
        {
            OnIngredientAddCall(ingredient);
        }
        
        private void Create()
        {
            if(MachineState == MachineState.Ready) return;
            PlantBase currentPlant = IngredientsSlots[0].Slot.MachineSlot.item as PlantBase;
            //var plant = Instantiate(currentPlant.MixedPlant, pos.position,
            //    Quaternion.identity, pos);
        }

        public void Hold()
        {
            if (!CanHit) return;
            if (MachineState is MachineState.Ready or MachineState.Empty) return;
            Debug.Log("oi");
            Holding = true;
        }

        private void FixedUpdate()
        {
            if (!Holding) return;
            if (CanHit)
                Hit();
        }

        public void StopHold()
        {
            Holding = false;
        }

        private void Hit()
        {
            LabEvents.OnItemMixedCall();
            StartCoroutine(ResetCoolDown());
        }
        public IEnumerator ResetCoolDown()
        {
            CanHit = false;
            yield return new WaitForSeconds(_coolDown);
            CanHit = true;
        }
        public void AddHits()
        {
            if (MachineState is MachineState.Ready or MachineState.Empty)
            {
                Holding = false;
            }
            
            Debug.Log("hit");
            _currentHits++;
            progressBar.AddCurrentValue(1);
            if (_currentHits != _hitsNecessaries) return;
            MachineState = MachineState.Ready;
            CreateResult();
        }
        
        public override void CheckFinishMachine(BaseMachineSlot slot)
        {
            //test if has to remove machine here
            slot.SetType(MachineSlotType.Ingredient);
            if (CheckIfCollectedAllResults())
            {
                SetState(MachineState.Empty);
                  
                if (LabTimeController.Instance.LabTimer.ContainsKey(LabEvents.CurrentMachine.MachineId))
                {
                    LabTimeController.Instance.LabTimer.Remove(LabEvents.CurrentMachine.MachineId);
                }
            }
        }

        private List<IngredientsList> GenerateToppingList()
        {
            List<IngredientsList> aux = new List<IngredientsList>();
            foreach (KeyValuePair<IngredientToppingSO, int> key in decoratorsAdded)
            {
                IngredientsList obj = new IngredientsList(key.Key, key.Value);
                aux.Add(obj);
            }

            return aux;
        }
    }
}