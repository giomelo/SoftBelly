using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class Pestle : BaseMachine, IMix
    {
        [SerializeField]
        private PestleObject PestleObj;
        [SerializeField]
        private Transform ingredientPos;

        private int _hitsNecessaries = 5;
        private int _currentHits;
        
        private List<GameObject> itemSmashed = new List<GameObject>();
        
        public override void CreateResult()
        {
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item == null) continue;
                
                var newSmashedPlant = ScriptableObject.CreateInstance<PlantBase>();
                
                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                newSmashedPlant.name = currentPlant.ItemId + "Smashed";
                newSmashedPlant.Init(currentPlant.ItemId + "Smashed", IngredientsSlots[i].Slot.MachineSlot.item.ItemType, currentPlant.SmashedPlant.SmashedPlantImage,currentPlant.Price, currentPlant.ItemProprietiesGO);
                IngredientsSlots[i].Slot.Image.sprite = newSmashedPlant.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = newSmashedPlant;
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

        public void Work()
        {
            if (MachineState is MachineState.Working or MachineState.Ready) return;
            _currentHits = 0;
            PestleObj.StartDrag();
            SetState(MachineState.Working);
            Create();
        }

        public void Create()
        {
            if(MachineState == MachineState.Ready) return;
            PlantBase currentPlant = IngredientsSlots[0].Slot.MachineSlot.item as PlantBase;
            var plant = Instantiate(currentPlant.SmashedPlant.SmashedPlantObj, ingredientPos.position,
                Quaternion.identity, ingredientPos);
            itemSmashed.Add(plant);
            //checa se foi o primeiro item criado
            if (itemSmashed.Count <= 1) return;
            
            foreach (var item in itemSmashed)
            {
                Vector3 scale = item.transform.localScale;
                item.transform.localScale = scale * 0.8f;
            }
        }
        
        // destroi todos os itens criados pela maquina
        public void ResetMachine()
        {
            for (int i = 0; i < ingredientPos.childCount; i++)
            {
                Destroy(ingredientPos.GetChild(i).gameObject);
            }

            itemSmashed.Clear();
        }
        
        public void OnDisposeMachine()
        {
            ResetMachine();
            SetState(MachineState.Empty);
            PestleObj.ResetObj();
        }

        public void AddHits()
        {
            _currentHits++;
            if (_currentHits != _hitsNecessaries) return;
            MachineState = MachineState.Ready;
            CreateResult();
        }

        private void OnEnable()
        {
            LabEvents.OnMachineSelected += OnCLicked;
            LabEvents.OnMachineDispose += OnDispose;
            LabEvents.OnMachineStarted += MachineProcess;
            IsDestroyed = false;
            LabEvents.OnItemSmashed += AddHits;
            LabEvents.OnItemSmashed += Create;
        }
        private void OnDisable()
        {
            LabEvents.OnMachineSelected -= OnCLicked;
            LabEvents.OnMachineDispose -= OnDispose;
            LabEvents.OnMachineStarted -= MachineProcess;
            IsDestroyed = true;
            LabEvents.OnItemSmashed -= AddHits;
            LabEvents.OnItemSmashed -= Create;
        }

        protected override void FinishMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCamera();
            //Pestle pesltle = LabEvents.CurrentMachine as Pestle;
            OnDisposeMachine();
        }

        protected override void InitMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCamera();
        }
        
        public override void CheckFinishMachine(BaseMachineSlot slot)
        {
            //test if has to remove machine here
            slot.SetType(MachineSlotType.Ingredient);
            if (CheckIfCollectedAllResults())
            {
                SetState(MachineState.Empty);
            }
        }
    }
}
