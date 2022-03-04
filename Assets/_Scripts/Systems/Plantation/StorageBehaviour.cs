using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plantation
{
   /// <summary>
   /// Behavior for all types of storage, handles the main methods like addItens, removeItens, searchItens
   /// </summary>
   public abstract class StorageBehaviour : ScriptableObject
   {
      protected Dictionary<ItemBehaviour, int> Slots;
      public int maxAmountPerSlots = 20;

      protected StorageBehaviour(Dictionary<ItemBehaviour, int> slots)
      {
         Slots = slots;
      }
      protected StorageBehaviour()
      {
        
      }

      public void AddItem(ItemBehaviour key, int amount)
      {
         if (Slots.ContainsKey(key))
         {
            Slots[key] += amount;
         }
         else
         {
            Slots.Add(key, amount);
         }
      }

      public void RemoveItem(ItemBehaviour key, int amount)
      {
         Slots[key] -= amount;
         if (Slots[key] <= 0)
         {
            Slots.Remove(key);
         }
      }

      public void Display()
      {
         foreach (var (key, value) in Slots)
         {
            Debug.Log(key.ItemId);
            Debug.Log(value);

         }
      }

      public bool CheckIfSlotIsFull(ItemBehaviour key)
      {
         var amountInSlot = Slots[key];
         var value = amountInSlot + 1;
         return value > maxAmountPerSlots;
      }
   }
}