using System.Collections.Generic;
using _Scripts.Editor.FlagsAtributeEditor;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
   /// <summary>
   /// Behavior for all types of storage, handles the main methods like addItens, removeItens, searchItens
   /// </summary>
   public abstract class StorageBehaviour : ScriptableObject
   {
      public Dictionary<ItemBehaviour, int> Slots;
      public int maxAmountPerSlots = 20;
      [EnumFlagsAtribute]
      public ItemType itensType; 
      
      [SerializeField] public int Width = 4;
      [SerializeField] public int Height = 4;
      protected StorageBehaviour(Dictionary<ItemBehaviour, int> slots)
      {
         Slots = slots;
      }
      protected StorageBehaviour()
      {
        
      }

      public void AddItem(ItemBehaviour key, int amount)
      {
         if (!itensType.HasFlag(key.ItemType)) return;
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
         // if (Slots[key] <= 0)
         // {
         //    Slots.Remove(key);
         // }
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