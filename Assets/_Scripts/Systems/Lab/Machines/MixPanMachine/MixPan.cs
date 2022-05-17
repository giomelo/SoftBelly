﻿using System;
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
using UnityEngine.UI;

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

        public static Action<IngredientObj> OnIngredientAdd;
        private Dictionary<IngredientObj, int> decoratorsAdded = new Dictionary<IngredientObj, int>(); //lista para adicionar todos os ingredientes
        [SerializeField]
        private ProgressBar progressBar;

        private float _coolDown = 1f;
        private int _currentHits;
        private int _hitsNecessaries = 10;

        public bool CanHit { get; set; }

        public override void Start()
        {
            base.Start();
            resetButton.onClick.AddListener(ResetMachine);
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

        private void AddText(IngredientObj ingredient)
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
        public static void OnIngredientAddCall(IngredientObj i)
        {
            OnIngredientAdd?.Invoke(i);
        }
        public override void CreateResult()
        {
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item == null) continue;
                
                var newMixedPlant = ScriptableObject.CreateInstance<PlantBase>();
                
                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                newMixedPlant.name = currentPlant.ItemId + "Mixed " + decoratorsText.text;

                newMixedPlant.Init(newMixedPlant.name + "Mixed " +  decoratorsText.text, IngredientsSlots[i].Slot.MachineSlot.item.ItemType, currentPlant.MixedPlant.MixedPlantImage,currentPlant.Price, currentPlant.ItemProprieties.ItemProprietiesGO, newMixedPlant.name + "Mixed " +  decoratorsText.text);
                IngredientsSlots[i].Slot.Image.sprite = newMixedPlant.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = newMixedPlant;
                IngredientsSlots[i].Slot.Amount.text = 1.ToString();
                IngredientsSlots[i].Slot.MachineSlot.amount = 1;
            }

            if(MachineState == MachineState.Ready)
            {
                SetSlotType();
            }

            ResetMachine();
            LabEvents.OnMachineFinishedCall(this);
        }

        private void ResetMachine()
        {
            ResetText();
            ResetBar();
        }

        private void ResetBar()
        {
            progressBar.SetCurrentValue(0);
        }

        private void SetText()
        {
            decoratorsText.text = "";
            foreach (KeyValuePair<IngredientObj, int> key in decoratorsAdded)
            {
                decoratorsText.text += " - " + key.Value + "x" + " " + key.Key.IngredientDescription;
            }
        }
        
        protected override void InitMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraMix();
            ingredietnsShelf.SetActive(true);
        }
        protected override void FinishMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraMix();
            OnDisposeMachine();
            ingredietnsShelf.SetActive(false);
        }

        private void OnDisposeMachine()
        {
            
        }

        public void Work()
        {
            if (MachineState is MachineState.Working or MachineState.Ready) return;
            
            foreach (var ingredient in decorators)
            {
                ingredient.StartDrag();
            }
            spoonObj.StartDrag();
            SetState(MachineState.Working);
            Create();
        }
        
        private void Create()
        {
            if(MachineState == MachineState.Ready) return;
            PlantBase currentPlant = IngredientsSlots[0].Slot.MachineSlot.item as PlantBase;
            var plant = Instantiate(currentPlant.MixedPlant.MixedPlantObject, pos.position,
                Quaternion.identity, pos);
        }

        public IEnumerator ResetCoolDown()
        {
            CanHit = false;
            yield return new WaitForSeconds(_coolDown);
            CanHit = true;
        }
        public void AddHits()
        {
            _currentHits++;
            if (_currentHits != _hitsNecessaries) return;
            MachineState = MachineState.Ready;
            CreateResult();
        }
    }
}