using System.Collections.Generic;
using System.Linq;
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
      public Dictionary<int, ItemObj> Slots;
      public int maxAmountPerSlots = 20;
      [EnumFlagsAtribute]
      public ItemType itensType;

      [SerializeField] 
      public int Width;
      [SerializeField] 
      public int Height;
      protected StorageBehaviour(Dictionary<int, ItemObj> slots)
      {
         Slots = slots;
      }
      protected StorageBehaviour()
      {
        
      }

      public void AddItem(int key, int amount, ItemBehaviour item)
      {
         if (!itensType.HasFlag(item.ItemType)) return;
         if (Slots.ContainsKey(key))
         {
            if (CheckIfSlotIsFull(key))
            {
               Debug.Log("Full");
               Slots.Add(key, new ItemObj(item, amount));
            }
            var auxObj = Slots[key];
            auxObj.amount += amount;
            Slots[key] = auxObj;
         }
         else
         {
            Slots.Add(key, new ItemObj(item, amount));
         }
      }

      public void RemoveItem(int key, int amount)
      {
         var auxObj = Slots[key];
         auxObj.amount -= amount;
         Slots[key] = auxObj;
         // if (Slots[key] <= 0)
         // {
         //    Slots.Remove(key);
         // }
      }

      public void Display()
      {
         foreach (var (key, value) in Slots)
         {
            Debug.Log(value.item.ItemId);
            Debug.Log(value);

         }
      }

      public bool CheckIfSlotIsFull(int key)
      {
         var amountInSlot = Slots[key].amount;
         Debug.Log(Slots[key]);
         var value = amountInSlot + 1;
         return value > maxAmountPerSlots;
      }

      private bool CheckIfContainsKey(ItemBehaviour key)
      {
         for(int i = 0; i < Slots.Count; i++)
         {
            // if (Slots.ElementAt(i).Key == key)
            // {
            //    return true;
            // }
         }

         return false;
      }
   }
}