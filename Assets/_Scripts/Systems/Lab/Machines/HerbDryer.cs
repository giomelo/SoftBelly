using System.Collections.Generic;
using _Scripts.Systems.Inventories;
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
                Debug.Log(ResultsSlots[i].Slot);
                var newDriedPlant = ScriptableObject.CreateInstance<PlantBase>();
                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                newDriedPlant.Init("DriedPlant", IngredientsSlots[i].Slot.MachineSlot.item.ItemType, currentPlant.DriedPlant,currentPlant.Price, currentPlant.ItemProprietiesGO);
                IngredientsSlots[i].Slot.Image.sprite = newDriedPlant.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = newDriedPlant;
                IngredientsSlots[i].Slot.Amount.text = 1.ToString();
                // ResultsSlots[i].Slot.MachineSlot.item = CurrentRecipe.Results[i].item;
                // ResultsSlots[i].Slot.MachineSlot.amount = CurrentRecipe.Results[i].amount;
            }
        }
    }
}