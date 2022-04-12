using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.Base;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MachineBehaviour
{
    public abstract class Burn : BaseMachine
    {
        public ItemObj BurnedResult;
        public int BurnTime;

        public virtual void CreateBurnedResult()
        {
            for (int i = 0; i < ResultsSlots.Count; i++)
            {
                ResultsSlots[i].Slot.Image.sprite = BurnedResult.item.ImageDisplay;
                ResultsSlots[i].Slot.Amount.text = BurnedResult.amount.ToString();
                ResultsSlots[i].Slot.MachineSlot.item = BurnedResult.item;
                ResultsSlots[i].Slot.MachineSlot.amount = BurnedResult.amount;
            }

            for (int i = 0; i < IngredientsSlots.Count; i++)
            {
                IngredientsSlots[i].ResetSlot();
            }
        }
    }
    
}