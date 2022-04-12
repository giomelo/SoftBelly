using System.Collections.Generic;
using _Scripts.Enums;
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
        private List<GameObject> plantsPos = new List<GameObject>();
        private List<ItemObj> ingredients = new List<ItemObj>();
        public void Work()
        {
            //se o usuário clicou no botão e a máquina ainda está exibindo os resultados
            if (LabEvents.CurrentMachine != null && LabEvents.CurrentMachine.MachineState == MachineState.Ready) return;
            
            foreach (var slotMachineObj in IngredientsSlots)
            {
                ingredients.Add(slotMachineObj.Slot.MachineSlot);
            }
            StartMachine();

        }

        private void PlacePlants()
        {
            
        }

        protected override void StartMachine()
        {
            LabEvents.OnMachineStartedCall(this);
            PlacePlants();
            StartTime();
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