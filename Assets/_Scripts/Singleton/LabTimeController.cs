using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;
using _Scripts.Systems.Lab.Recipes;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;

namespace _Scripts.Singleton
{
    /// <summary>
    /// Timer controller for machines that need a timer
    /// </summary>
    public class LabTimeController : MonoSingleton<LabTimeController>
    {
        public Dictionary<int, MachineStoreValues> LabTimer { get; } = new Dictionary<int,MachineStoreValues>();
        
        public void AddTime(int machineId, float time, RecipeObj recipe)
        {
            LabTimer.Add(machineId, new MachineStoreValues(recipe, time));
        }

        public void ClearSlot(int id)
        {
            LabTimer.Remove(id);
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        public void DisplayMachines()
        {
            if (LabTimer.Count == 0)  return;
            for (int i = 0; i < LabTimer.Count; i++)
            {
                foreach (var machine in MachineSystemController.Instance.allMachines.Where(t => LabTimer.ElementAt(i).Key == t.CurrentMachine.MachineId))
                {
                    machine.CurrentMachine.CurrentRecipe = LabTimer.ElementAt(i).Value.CurrentRecipeObj;
                    machine.CurrentMachine.CreateResult();
                }
            }
        }
        public IEnumerator WorkMachine(BaseMachine machine)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("WorkingMachine");
            var aux = LabTimer[machine.MachineId].Time;
            aux -= 1;
            var p = new MachineStoreValues(machine.CurrentRecipe, aux);
            LabTimer[machine.MachineId] = p;
            
            if (LabTimer[machine.MachineId].Time <= 0)
            {
                if (machine.IsDestroyed) yield break;
                machine.SetState(MachineState.Ready);
                machine.CreateResult();
            }
            else
            {
                StartCoroutine(WorkMachine(machine));
            }
        }
    }
}