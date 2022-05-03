using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class Cauldron : Burn, ITimerMachine
    {
        
        public override void CreateResult()
        {
            for (int i = 0; i < ResultsSlots.Count; i++)
            {
                ResultsSlots[i].Slot.Image.sprite = CurrentRecipe.Results[i].item.ImageDisplay;
                ResultsSlots[i].Slot.Amount.text = CurrentRecipe.Results[i].amount.ToString();
                ResultsSlots[i].Slot.MachineSlot.item = CurrentRecipe.Results[i].item;
                ResultsSlots[i].Slot.MachineSlot.amount = CurrentRecipe.Results[i].amount;
            }

            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                IngredientsSlots[i].ResetSlot();
            }

            LabEvents.OnMachineFinishedCall(this);
        }

        public void Work()
        {
            StartMachine();
        }

        public override void CheckFinishMachine(BaseMachineSlot slot)
        {
            if (CheckIfCollectedAllResults())
            {
                LabTimeController.Instance.LabTimer.Remove(MachineId);
                SetState(MachineState.Empty);
            }
        }
    }
}