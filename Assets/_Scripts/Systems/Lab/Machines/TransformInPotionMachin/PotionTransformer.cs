using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.Systems.Plants.Bases;
using JetBrains.Annotations;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.TransformInPotionMachin
{
    [Serializable]
    public struct PotionsTypes
    {
        public PotionType PotionType;
        public List<MachinesTypes> MachinesTypes;
        public List<IngredientsList> IngredientsList;
    }
    public class PotionTransformer : BaseMachine
    {
        [SerializeField]
        private List<PotionsTypes> PotionsTypes;
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
                string symtoms = GenerateSymptomsDescription(currentPlant.MedicalSymptoms);
                Debug.Log(symtoms);
                PotionType type;
                if(mixedPlant == null)
                    type = CreatePotionType(currentPlant.MachineList, null);
                else
                    type = CreatePotionType(currentPlant.MachineList, mixedPlant.IngredientsList);
                potion.Init(potion.name, ItemType.Potion, currentPlant.PotionStuff.PotionSprite,currentPlant.Price, currentPlant.ItemProprieties.ItemProprietiesGO, potion.name + " " + symtoms + " " + type, currentPlant.MedicalSymptoms, 
                    type);

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

        private string GenerateSymptomsDescription(List<SymptomsNivel> symptoms)
        {
            string returnvalue = "";
            foreach (var value in symptoms)
            {
                returnvalue += " " + value.Symptoms + "-" + value.Nivel;
            }
            // foreach (var value in Enum.GetValues(typeof(MedicalSymptoms)))
            // {
            //     if (!symptoms.HasFlag((MedicalSymptoms) value)) continue;
            //     Console.WriteLine((MedicalSymptoms) value);
            //     returnvalue += " " + (MedicalSymptoms) value;
            // }

            return returnvalue;
        }

        protected override void InitMachine()
        {
            
        }
        protected override void FinishMachine()
        {
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

        private PotionType CreatePotionType(List<MachinesTypes> types,  [CanBeNull] List<IngredientsList> ingredientsList)
        {
            PotionType potionType = PotionType.Wrong;
            foreach (var type in PotionsTypes)
            {
                if (!types.SequenceEqual(type.MachinesTypes)) continue;
                if (ingredientsList != null)
                {
                    if (ingredientsList.SequenceEqual(type.IngredientsList))
                        potionType = type.PotionType;
                    
                }
                else
                    potionType = type.PotionType;
                
            }

            return potionType;
        }
    }
}
