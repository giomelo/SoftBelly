﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
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

        public List<ItemObj> HerbIngredientsSlot = new List<ItemObj>();
        public List<ItemObj> CaldruonIngredientsSlot = new List<ItemObj>();
        public List<ItemObj> CaldruonResultsSlot = new List<ItemObj>();
        public void AddTime(int machineId, float time, RecipeObj recipe)
        {
            LabTimer.Add(machineId, new MachineStoreValues(recipe, time));
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
        /// <summary>
        /// Herb machine result depends on the variables in the machine slot, so in start i have to set the slot to the
        /// the previous slot
        /// </summary>
        /// <param name="machine"></param>
        private void SetSlotsHerbDryer(HerbDryer machine)
        {
            for (int i =0; i< HerbIngredientsSlot.Count; i++)
            {
                machine.IngredientsSlots[i].Slot.MachineSlot = HerbIngredientsSlot[i];
            }
        }
        /// <summary>
        /// Cauldroun machine result depends on the variables in the machine slot, so in start i have to set the slot to the
        /// the previous slot
        /// </summary>
        /// <param name="machine"></param>
        private void SetSlotsCaldruon(Cauldron machine)
        {
            for (int i =0; i< CaldruonIngredientsSlot.Count; i++)
            {
                machine.IngredientsSlots[i].Slot.MachineSlot = CaldruonIngredientsSlot[i];
            }
            
            for (int i =0; i< CaldruonResultsSlot.Count; i++)
            {
                machine.ResultsSlots[i].Slot.MachineSlot = CaldruonResultsSlot[i];
            }
        }
        /// <summary>
        /// Display machines in start
        /// </summary>
        public void DisplayMachines()
        {
            if (LabTimer.Count == 0)  return;
            for (int i = 0; i < LabTimer.Count; i++)
            {
                foreach (var machine in MachineSystemController.Instance.allMachines.Where(t => LabTimer.ElementAt(i).Key == t.CurrentMachine.MachineId))
                {
                    if (machine.CurrentMachine as HerbDryer)
                    {
                        HerbDryer herbDryer = machine.CurrentMachine as HerbDryer;
                        SetSlotsHerbDryer(herbDryer);
                        herbDryer.UpdatePlantObjects();
                    }
                    else
                    {
                        if (machine.CurrentMachine as Cauldron)
                        {
                            Cauldron cauldron = machine.CurrentMachine as Cauldron;
                            SetSlotsCaldruon(cauldron);
                        }
                        
                    }
                    if (LabTimer[machine.CurrentMachine.MachineId].Time > 0)
                    {
                        machine.CurrentMachine.MachineState = MachineState.Working;
                        machine.CurrentMachine.MachineProcess(machine.CurrentMachine);
                    }
                    else
                    {
                        machine.CurrentMachine.MachineState = MachineState.Ready;
                        machine.CurrentMachine.CurrentRecipe = LabTimer[machine.CurrentMachine.MachineId].CurrentRecipeObj;
                        machine.CurrentMachine.CreateResult();
                        machine.CurrentMachine.MachineProcess(machine.CurrentMachine);
                        if (machine.CurrentMachine.CanBurn)
                        {
                            Burn burnMachine = machine.CurrentMachine as Burn;
                            if (LabTimer[machine.CurrentMachine.MachineId].Time <= -burnMachine.BurnTime)
                            {
                                //queimou
                                burnMachine.CreateBurnedResult();
                                machine.CurrentMachine.MachineProcess(machine.CurrentMachine);
                            }
                            return;
                        }
                    }
                    //machine.CurrentMachine.MachineState = MachineState.Ready;
                }
            }
            
            
        }

        public void PrintLabTimer()
        {
            foreach (var x in LabTimer)
            {
                
                Debug.LogWarning(x.Key + " " + x.Value);
                Debug.LogWarning(LabTimer.Count);
            }
        }
        public IEnumerator WorkMachine(MachineHolder machine)
        {
            yield return new WaitForSeconds(1f);
            
            //pega a maquina da cena
            IEnumerable<MachineHolder> sceneMachine = null;
            var currentMachine = machine.CurrentMachine;
            if (MachineSystemController.Instance != null)
            {
                sceneMachine = MachineSystemController.Instance.allMachines.Where(p => p.CurrentMachine.MachineId == currentMachine.MachineId);
                Debug.LogWarning("Maquinas: " + sceneMachine.Count());
                foreach(var machineAux in sceneMachine)
                {
                    currentMachine = machineAux.CurrentMachine;
                }
            }
            Debug.Log("WorkingMachine");
            //se a máquina parou de funcionar
            if(!LabTimer.ContainsKey(currentMachine.MachineId)) yield break;

            if (LabTimer[currentMachine.MachineId].Time > 0)
            {
                //subtrai o tempo da maquina
                var aux = LabTimer[currentMachine.MachineId].Time;
                aux -= 1;
                var p = new MachineStoreValues(LabTimer[currentMachine.MachineId].CurrentRecipeObj, aux);
                LabTimer[currentMachine.MachineId] = p;
                currentMachine.CurrentRecipe = LabTimer[currentMachine.MachineId].CurrentRecipeObj;
                Debug.Log("Time: " + LabTimer[currentMachine.MachineId].Time);
            }
            else
            {
                if (currentMachine.CanBurn)
                {
                    //subtrai o tempo da maquina
                    var aux = LabTimer[currentMachine.MachineId].Time;
                    aux -= 1;
                    var p = new MachineStoreValues(LabTimer[currentMachine.MachineId].CurrentRecipeObj, aux);
                    LabTimer[currentMachine.MachineId] = p;
                    currentMachine.CurrentRecipe = LabTimer[currentMachine.MachineId].CurrentRecipeObj;
                    Debug.Log("Time: " + LabTimer[currentMachine.MachineId].Time);
                }
            }

            //se o tempo passou de zero
            if (LabTimer[currentMachine.MachineId].Time <= 0)
            {
                //se a maquina nao existe na cena
                if (!currentMachine.IsDestroyed)
                {
                    if (currentMachine.MachineState == MachineState.Working)
                    {
                        if (currentMachine.MachineState != MachineState.Ready)
                        {
                            currentMachine.SetState(MachineState.Ready);
                            currentMachine.CreateResult();
                        }
                    }
                }

                //se a maquina pode queimar/passar do tempo ela continuar rodando
                if (currentMachine.CanBurn)
                {
                    Burn burnMachine = currentMachine as Burn;
                    if (LabTimer[currentMachine.MachineId].Time >= -burnMachine.BurnTime)
                    {
                        if(!LabTimer.ContainsKey(currentMachine.MachineId)) yield break;
                        StartCoroutine(WorkMachine(machine));
                    }
                    else
                    {
                        //queimou
                        if (currentMachine.IsDestroyed) yield break;
                        burnMachine.CreateBurnedResult();
                        LabTimer.Remove(currentMachine.MachineId);
                    }
                }
                else
                {
                    //LabTimer.Remove(currentMachine.MachineId);
                }
               
            }
            else
            {
                StartCoroutine(WorkMachine(machine));
            }
        }
    }
}