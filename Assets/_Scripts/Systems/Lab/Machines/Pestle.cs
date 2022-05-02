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
            throw new System.NotImplementedException();
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
            PlantBase currentPlant = IngredientsSlots[0].Slot.MachineSlot.item as PlantBase;
            var plant = Instantiate(currentPlant.SmashedPlant.SmashedPlantObj, ingredientPos.position,
                Quaternion.identity, ingredientPos);
            itemSmashed.Add(plant);
            if (itemSmashed.Count > 1)
            {
                foreach (var item in itemSmashed)
                {
                    Vector3 scale = item.transform.localScale;
                    item.transform.localScale = scale * 0.8f;
                }
            }
        }
        
        public void OnDisposeMachine()
        {
            SetState(MachineState.Empty);
            PestleObj.ResetObj();
        }

        public void AddHits()
        {
            _currentHits++;
            if (_currentHits == _hitsNecessaries)
            {
                MachineState = MachineState.Ready;
            }
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
    }
}
