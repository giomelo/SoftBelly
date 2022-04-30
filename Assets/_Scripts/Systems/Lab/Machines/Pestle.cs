using System;
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
        private PestleObject _pestle;
        [SerializeField]
        private Transform ingredientPos;

        private int _hitsNecessaries;
        public override void CreateResult()
        {
            throw new System.NotImplementedException();
        }

        public void Work()
        {
            _pestle.StartDrag();
            SetState(MachineState.Working);
            PlantBase currentPlant = IngredientsSlots[0].Slot.MachineSlot.item as PlantBase;
            if (currentPlant != null)
            {
                var plant = Instantiate(currentPlant.SmashedPlant.SmashedPlantObj, ingredientPos.position,
                    Quaternion.identity);
            }
        }

        public void OnDisposeMachine()
        {
            SetState(MachineState.Empty);
            _pestle.ResetObj();
        }
    }
}
