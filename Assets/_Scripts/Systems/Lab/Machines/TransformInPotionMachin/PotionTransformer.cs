using System;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.TransformInPotionMachin
{
    public class PotionTransformer : BaseMachine
    {
        public override void CreateResult()
        {
            MachineState = MachineState.Ready;
            Debug.LogWarning("Results!!");
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item == null) continue;
                
                var potion = ScriptableObject.CreateInstance<PotionBase>();
                PlantBase currentPlant = null;
                BaseMixedItem mixedPlant = null;
                switch (IngredientsSlots[i].Slot.MachineSlot.item.ItemType)
                {
                    case ItemType.MixedPlant:
                        mixedPlant = IngredientsSlots[i].Slot.MachineSlot.item as BaseMixedItem;
                        currentPlant = mixedPlant.BasePlant;
                        break;
                    case ItemType.Burned:
                        currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                        break;
                }

                potion.name = currentPlant.ItemId + " Potion ";
                string symtoms = GenerateSymtompsDescription(currentPlant.MedicalSymptoms);
                Debug.Log(symtoms);
                potion.Init(potion.name, ItemType.Potion, currentPlant.PotionStuff.PotionSprite,currentPlant.Price, currentPlant.ItemProprieties.ItemProprietiesGO, potion.name + " " + symtoms, mixedPlant.IngredientsList, currentPlant.MedicalSymptoms);
                IngredientsSlots[i].Slot.Image.sprite = potion.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = potion;
                IngredientsSlots[i].Slot.Amount.text = 1.ToString();
                IngredientsSlots[i].Slot.MachineSlot.amount = 1;
            }

            if(MachineState == MachineState.Ready)
            {
                SetSlotType();
            }

            RestartMachine();
            LabEvents.OnMachineFinishedCall(this);
        }

        private void RestartMachine()
        {
            
        }

        private string GenerateSymtompsDescription(MedicalSymptoms symptoms)
        {
            string returnvalue = "";
            foreach (var value in Enum.GetValues(typeof(MedicalSymptoms)))
            {
                if (!symptoms.HasFlag((MedicalSymptoms) value)) continue;
                Console.WriteLine((MedicalSymptoms) value);
                returnvalue += " " + (MedicalSymptoms) value;
            }

            return returnvalue;
        }

        protected override void InitMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraPotion();
        }
        protected override void FinishMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraPotion();
            OnDisposeMachine();
        }

        private void OnDisposeMachine()
        {
            MachineState = MachineState.Empty;
        }
        public void Work()
        {
            if (MachineState is MachineState.Working or MachineState.Ready) return;
            SetState(MachineState.Working);
            CreateResult();
        }
        
        public override void CheckFinishMachine(BaseMachineSlot slot)
        {
            //test if has to remove machine here
            slot.SetType(MachineSlotType.Ingredient);
            if (CheckIfCollectedAllResults())
            {
                SetState(MachineState.Empty);
            }
        }
    }
}
