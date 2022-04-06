﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Recipes;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Singleton
{

    /// <summary>
    /// Timer controller for machines that need a timer
    /// </summary>
    public class LabTimeController : MonoSingleton<LabTimeController>
    {
        public Dictionary<int, MachineStoreValues> LabTimer { get; } = new Dictionary<int, MachineStoreValues>();
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
                    if (LabTimer[machine.CurrentMachine.MachineId].Time >= 0)
                    {
                        machine.CurrentMachine.MachineState = MachineState.Working;
                        machine.CurrentMachine.MachineProcess(machine.CurrentMachine);
                        return;
                    }
                    machine.CurrentMachine.CurrentRecipe = LabTimer[machine.CurrentMachine.MachineId].CurrentRecipeObj;
                    machine.CurrentMachine.CreateResult();
                    //machine.CurrentMachine.MachineState = MachineState.Ready;
                }
            }
            
            
        }
        public IEnumerator WorkMachine(BaseMachine machine)
        {
            yield return new WaitForSeconds(1f);
            IEnumerable<MachineHolder> sceneMachine = null;
            if (MachineSystemController.Instance != null)
            {
                sceneMachine = MachineSystemController.Instance.allMachines.Where(p => p.CurrentMachine.MachineId == machine.MachineId);
                foreach(var machineAux in sceneMachine)
                {
                    machine = machineAux.CurrentMachine;
                }
            }
            
            Debug.Log("WorkingMachine");
            var aux = LabTimer[machine.MachineId].Time;
            aux -= 1;
            var p = new MachineStoreValues(LabTimer[machine.MachineId].CurrentRecipeObj, aux);
            Debug.Log("Time: " + LabTimer[machine.MachineId].Time);
            LabTimer[machine.MachineId] = p;
            Debug.Log(sceneMachine);
            machine.CurrentRecipe = LabTimer[machine.MachineId].CurrentRecipeObj;
            
            if (LabTimer[machine.MachineId].Time <= 0)
            {
                LabTimer.Remove(machine.MachineId);//remove to make machine brun the igrerdient
                if (machine.IsDestroyed) yield break;
                machine.SetState(MachineState.Ready);
                Debug.LogWarning(machine);
                
                machine.CreateResult();
            }
            else
            {
                StartCoroutine(WorkMachine(machine));
            }
        }
    }
}