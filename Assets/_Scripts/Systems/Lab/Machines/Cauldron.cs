using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class Cauldron : BaseMachine, IBurn
    {
        public void Burn()
        {
            StartMachine();
        }

        public override void CreateResult()
        {
            for (int i = 0; i < ResultsSlots.Count; i++)
            {
                Debug.Log("Results");
                Debug.Log(ResultsSlots[i].Slot);
                Debug.Log(CurrentRecipe);
                Debug.Log(ResultsSlots[i]);
                ResultsSlots[i].Slot.Image.sprite = CurrentRecipe.Results[i].item.ImageDisplay;
                ResultsSlots[i].Slot.Amount.text = CurrentRecipe.Results[i].amount.ToString();
                ResultsSlots[i].Slot.MachineSlot.item = CurrentRecipe.Results[i].item;
                ResultsSlots[i].Slot.MachineSlot.amount = CurrentRecipe.Results[i].amount;
            }

            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                IngredientsSlots[i].ResetSlot(IngredientsSlots[i].Slot);
            }

            LabEvents.OnMachineFinishedCall(this);
        }
    }
}