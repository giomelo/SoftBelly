﻿using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class Cauldron : Burn, ITimerMachine
    {
        [HideInInspector]
        public List<ItemObj> ingredients = new List<ItemObj>();
        [HideInInspector]
        public List<ItemObj> results = new List<ItemObj>();

        private PlantTemperature currentTemperature = PlantTemperature.Low;
        [SerializeField]
        private Transform[] tempButtons = new Transform[3];
        [SerializeField]
        private Transform seta;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void CreateResult()
        {
            Debug.LogWarning("Results!!");
            // for (int i = 0; i < ResultsSlots.Count; i++)
            // {
            //     ResultsSlots[i].Slot.Image.sprite = CurrentRecipe.Results[i].item.ImageDisplay;
            //     ResultsSlots[i].Slot.Amount.text = CurrentRecipe.Results[i].amount.ToString();
            //     ResultsSlots[i].Slot.MachineSlot.item = CurrentRecipe.Results[i].item;
            //     ResultsSlots[i].Slot.MachineSlot.amount = CurrentRecipe.Results[i].amount;
            // }
            //
            // for (int i = 0; i < IngredientsSlots.Count; i++)
            // {
            //     IngredientsSlots[i].ResetSlot();
            // }
            //
            // LabEvents.OnMachineFinishedCall(this);
            
            for (int i = 0; i < ResultsSlots.Count; i++)
            {
                var newBurned = ScriptableObject.CreateInstance<PlantBase>();

                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
     
                newBurned.name = currentPlant.ItemId + "Burned";
                newBurned.Init(currentPlant.ItemId + " Queimada", ItemType.Burned, currentPlant.BurnedPlant,currentPlant.Price, newBurned.name,  currentPlant.BurnedPlant,currentPlant.MixedPlant, currentPlant.DriedPlant, currentPlant.PotionStuff, 
                    currentPlant.SmashedPlant, currentPlant.MedicalSymptoms, currentPlant.plantTemperature, currentPlant.MachineList);
          
                ResultsSlots[i].Slot.Image.sprite = newBurned.ImageDisplay;
                ResultsSlots[i].Slot.MachineSlot.item = newBurned;
                ResultsSlots[i].Slot.Amount.text = 1.ToString();
                ResultsSlots[i].Slot.MachineSlot.amount = 1;
                newBurned.MachineList = new List<MachinesTypes>();
                foreach (var type in currentPlant.MachineList)
                {
                    newBurned.AddMachine(type);
                }
                newBurned.AddMachine(MachineTypes);

                foreach (var wd in newBurned.MachineList)
                {
                   Debug.Log(wd);
                }
            }

            SetSlotResults();
            StartCoroutine(voltarSlot());
            for (int i = 0; i < ResultsSlots.Count; i++)
            {
                results.Add(ResultsSlots[i].Slot.MachineSlot);
            }
            LabEvents.OnMachineFinishedCall(this);
        }

        IEnumerator voltarSlot()
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                IngredientsSlots[i].ResetSlot();
            }
        }

        public void ChangeTemperature(int temp)
        {
            currentTemperature = (PlantTemperature)temp;
           
            LookAtSeta(tempButtons[(int) currentTemperature]);
        }

        private void Start()
        {
            base.Start();
            LookAtSeta(tempButtons[(int) currentTemperature]);
        }

        private void LookAtSeta(Transform pos)
        {
            seta.LookAt(pos);
        }

        public void Work()
        {
            //se o usuário clicou no botão e a máquina ainda está exibindo os resultados
            if (LabEvents.CurrentMachine != null && MachineState == MachineState.Ready) return;
            //ser nao tem item
            if (!CheckIfHasItem()) return;
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                ingredients.Add(IngredientsSlots[i].Slot.MachineSlot);
            }
            StartMachine();
        }

        public override void CheckFinishMachine(BaseMachineSlot slot)
        {
            if (CheckIfCollectedAllResults())
            {
                LabTimeController.Instance.LabTimer.Remove(MachineId);
                SetState(MachineState.Empty);
                  
                if (LabTimeController.Instance.LabTimer.ContainsKey(LabEvents.CurrentMachine.MachineId))
                {
                    LabTimeController.Instance.LabTimer.Remove(LabEvents.CurrentMachine.MachineId);
                }
            }
        }
        private void SetSlotsLabTimer()
        {
            foreach (var slot in IngredientsSlots)
            {
                LabTimeController.Instance.CaldruonIngredientsSlot.Add(slot.Slot.MachineSlot);
            }
        }
        private void SetSlotResults()
        {
            foreach (var slot in ResultsSlots)
            {
                LabTimeController.Instance.CaldruonResultsSlot.Add(slot.Slot.MachineSlot);
            }
        }
        protected override void OnSlotDispose(BaseMachineSlot slot)
        {
            
        }

        protected override void StartMachine()
        {
            PlantBase currentPlant = IngredientsSlots[0].Slot.MachineSlot.item as PlantBase;
            if (currentPlant.plantTemperature != currentTemperature)
            {
                // selecionou temperatura errada
                switch (currentPlant.plantTemperature)
                {
                    case PlantTemperature.Low:
                        switch (currentTemperature)
                        {
                            case PlantTemperature.Medium:
                                machineWorkingTime -= 3;
                                break;
                            case PlantTemperature.High:
                                machineWorkingTime -= 4;
                                BurnTime -= 2;
                                break;
                        }
                        break;
                    case PlantTemperature.Medium:
                        switch (currentTemperature)
                        {
                            case PlantTemperature.Low:
                                machineWorkingTime += 2;
                                break;
                            case PlantTemperature.High:
                                machineWorkingTime -= 2;
                                break;
                        }
                        break;
                    case PlantTemperature.High:
                        switch (currentTemperature)
                        {
                            case PlantTemperature.Low:
                                machineWorkingTime += 5;
                                BurnTime += 2;
                                break;
                            case PlantTemperature.Medium:
                                machineWorkingTime += 3;
                                break;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            SetSlotsLabTimer();
            LabEvents.OnMachineStartedCall(this);
            StartTime();
        }
    }
}