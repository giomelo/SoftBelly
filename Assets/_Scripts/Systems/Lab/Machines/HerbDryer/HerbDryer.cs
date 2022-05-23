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
            if (LabEvents.CurrentMachine != null && MachineState == MachineState.Ready) return;
            //ser nao tem item
            if (!CheckIfHasItem()) return;
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
            if (plantsPos[index].transform.childCount <= 0) return;
            Destroy(plantsPos[index].GetChild(0).gameObject);
        }

        private void PlacePlants(int index)
        {
            PlantBase currentPlant = IngredientsSlots[index].Slot.MachineSlot.item as PlantBase;
            if (currentPlant != null)
            {
                var plant = Instantiate((Object) currentPlant.DriedPlant.DryingPlant, plantsPos[index].position,
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
            Debug.LogWarning("Results!");
            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                if (IngredientsSlots[i].Slot.MachineSlot.item == null) continue;
                
                var newDriedPlant = ScriptableObject.CreateInstance<PlantBase>();
                
                PlantBase currentPlant = IngredientsSlots[i].Slot.MachineSlot.item as PlantBase;
                newDriedPlant.name = currentPlant.ItemId + "Dried";
                newDriedPlant.Init(currentPlant.ItemId + "Dried", ItemType.Dryed, currentPlant.DriedPlant.DriedPlantImage,currentPlant.Price, currentPlant.ItemProprieties.ItemProprietiesGO,newDriedPlant.name, currentPlant.BurnedPlant,currentPlant.MixedPlant, currentPlant.DriedPlant, currentPlant.PotionStuff, currentPlant.SmashedPlant);
                IngredientsSlots[i].Slot.Image.sprite = newDriedPlant.ImageDisplay;
                IngredientsSlots[i].Slot.MachineSlot.item = newDriedPlant;
                IngredientsSlots[i].Slot.Amount.text = 1.ToString();
                IngredientsSlots[i].Slot.MachineSlot.amount = 1;
            }

     
            if(MachineState == MachineState.Ready)
            {
                SetSlotType();
            }
            
            LabEvents.OnMachineFinishedCall(this);
        }

        public override bool CheckIfSlotCanReciveIngredient()
        {
            if (!LabEvents.MachineSlot.itemRequired.HasFlag(LabEvents.IngredientSelected.ItemType))
            {
                return false;
            }

            if (!(LabEvents.IngredientSelected as PlantBase)) return true;
            var plant = LabEvents.IngredientSelected as PlantBase;
            
            return !(plant.ItemType == ItemType.Dryed);
        }

        protected override void OnSlotDispose(BaseMachineSlot slot)
        {
            
        }

        public override void CheckFinishMachine(BaseMachineSlot slot)
        {
            //test if has to remove machine here
            slot.SetType(MachineSlotType.Ingredient);
            RemovePlantObject(slot.Slot.slotId);
            if (CheckIfCollectedAllResults())
            {
                SetState(MachineState.Empty);
            }
        }
    }
}