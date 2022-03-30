using System.Collections.Generic;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class HerbDryer : BaseMachine, ITimerMachine
    {
        private List<GameObject> plantsPos = new List<GameObject>();
        public void Work()
        {
            List<ItemObj> ingredients = new List<ItemObj>();
            foreach (var slotMachineObj in IngredientsSlots)
            {
                ingredients.Add(slotMachineObj.Slot.MachineSlot);
            }
            StartMachine();
        }

        private void PlacePlants()
        {
            
        }
    }
}