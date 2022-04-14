using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class HerbDryer : BaseMachine, ITimerMachine
    {
        public List<Transform> plantsPos = new List<Transform>();
        [HideInInspector]
        public List<ItemObj> ingredients = new List<ItemObj>();
        public void Work()
        {
            //se o usuário clicou no botão e a máquina ainda está exibindo os resultados
            if (LabEvents.CurrentMachine != null && LabEvents.CurrentMachine.MachineState == MachineState.Ready) return;
            
            for (int i =0; i< IngredientsSlots.Count; i++)
            {
                ingredients.Add(IngredientsSlots[i].Slot.MachineSlot);
                if (IngredientsSlots[i].Slot.MachineSlot.item != null)
                {
                    PlacePlants(i);
                }
            }
            StartMachine();

        }

        public void UpdatePlantObjects()
        {
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item != null)
                {
                    PlacePlants(i);
                }
            }
        }

        public void RemovePlantObject(int index)
        {
            Destroy(plantsPos[index].GetChild(0).gameObject);
        }

        private void PlacePlants(int index)
        {
            PlantBase currentPlant = IngredientsSlots[index].Slot.MachineSlot.item as PlantBase;
            if (currentPlant != null)
            {
                var plant = Instantiate((Object) currentPlant.DryingPlant, plantsPos[index].position,
                    Quaternion.identity, plantsPos[index]);
            }
        }

        protected override void StartMachine()
        {
            SetSlotsLabTimer();
            LabEvents.OnMachineStartedCall(this);
            StartTime();
        }

        private void SetSlotsLabTimer()
        {
            foreach (var slot in IngredientsSlots)
            {
                LabTimeController.Instance.HerbIngredientsSlot.Add(slot.Slot.MachineSlot);
            }
        }

        public override void CreateResult()
        {
            Debug.Log("Created");
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item == null) continue;
                
                Debug.Log("Results");
                var newDriedPlant = ScriptableObject.CreateInstance<PlantBase>();
                
                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                newDriedPlant.name = currentPlant.ItemId + "Dried";
                newDriedPlant.Init(currentPlant.ItemId + "Dried", IngredientsSlots[i].Slot.MachineSlot.item.ItemType, currentPlant.DriedPlant,currentPlant.Price, currentPlant.ItemProprietiesGO);
                
                IngredientsSlots[i].Slot.Image.sprite = newDriedPlant.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = newDriedPlant;
                IngredientsSlots[i].Slot.Amount.text = 1.ToString();
                IngredientsSlots[i].Slot.MachineSlot.amount = 1;
            }

            foreach (var machine in MachineSystemController.Instance.allMachines)
            {
                if(machine.CurrentMachine == machine.CurrentMachine as HerbDryer)
                {
                    machine.CurrentMachine.SetSlotType();
                }
            }
            
            LabEvents.OnMachineFinishedCall(this);
        }
    }
}