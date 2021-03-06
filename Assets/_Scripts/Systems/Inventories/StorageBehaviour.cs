using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;
using System;

namespace _Scripts.Systems.Inventories
{
   /// <summary>
   /// Behavior for all types of storage, handles the main methods like addItens, removeItens, searchItens
   /// </summary>
   public abstract class StorageBehaviour : ScriptableObject
   {
      public Dictionary<int, ItemObj> Slots;
      public int maxAmountPerSlots = 20;
      [EnumFlagsAttribute]
      public ItemType itensType;

      [SerializeField] 
      public int Size;

      [Header("This id is for putting items in the inventory")]
      public int storageId;
      
      public InventoryType InventoryType;
      protected StorageBehaviour(Dictionary<int, ItemObj> slots)
      {
         Slots = slots;
      }
      protected StorageBehaviour()
      {
        
      }

      public void AddItem(int amount, ItemBehaviour item)
      {
         if (!itensType.HasFlag(item.ItemType))
         {
            Debug.LogWarning("Item não pode ser adicionado nesse inventário");
            return;
         }
         var index = CheckIfSlotAreAvailabe(item);
         if (CheckIfContainsKey(item))
         {
            index = CheckIfSlotAreAvailabe(item);
            if (index == Slots.Count)
            {
               Slots.Add(index, new ItemObj(item, amount));
               return;
            }
            var auxObj = Slots[index];
            auxObj.amount += amount;
            Slots[index] = auxObj;
         }
         else
         {
            Slots.Add(index, new ItemObj(item, amount));
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
      
      /// <summary>
      /// Find the slot with the minor amount and remove from it
      /// </summary>
      /// <param name="item"></param>
      public void RemoveItem(ItemBehaviour item)
      {
         bool first = true;
         var last = 0;
         var index = 0;
         // IOrderedEnumerable<KeyValuePair<int, ItemObj>> items = Slots.Where(slot => slot.Value.item == item)
         //    .OrderBy(slot => slot.Value.amount);
         // foreach (KeyValuePair<int, ItemObj> i in items)
         // {
         //    Debug.Log(items.);
         // }
         //
         // var auxObj = Slots[index];
         // auxObj.amount -= 1;
         // Slots[index] = auxObj;
         // for (int i = 0; i < items.Count(); i++)
         // {
         //    Debug.Log(i);
         //    if (first)
         //    {
         //       last = Slots.ElementAt(i).Value.amount;
         //       first = false;
         //    }
         //    if (Slots.ElementAt(i).Value.amount < last)
         //    {
         //       last = Slots.ElementAt(i).Value.amount;
         //       index = i;
         //    }
         //    
         //    var auxObj = Slots[index];
         //    auxObj.amount -= 1;
         //    Slots[index] = auxObj;
         // }
         for(int i = 0; i < Slots.Count; i++)
         {
            if (Slots.ElementAt(i).Value.item != item) continue;
            if (first)
            {
               last = Slots.ElementAt(i).Value.amount;
               first = false;
            }

            if (Slots.ElementAt(i).Value.amount >= last) continue;
            last = Slots.ElementAt(i).Value.amount;
            index = i;
         }
         
         var auxObj = Slots.ElementAt(index).Value;
         auxObj.amount -= 1;
         Slots[index] = auxObj;
      }

      public void Display()
      {
         foreach (var (key, value) in Slots)
         {
            //Debug.Log(value.item.ItemId);
            //Debug.Log(value);

         }
      }
      //Chek if there is space in the slot, else create new slot
      public int CheckIfSlotAreAvailabe(ItemBehaviour item)
      {
         for(int i = 0; i < Slots.Count; i++)
         {
            if (Slots.ElementAt(i).Value.item != item) continue;
            var amountInSlot = Slots.ElementAt(i).Value.amount;
            if (amountInSlot < maxAmountPerSlots)
            {
               return i;
            }
         }
         return Slots.Count;
      }

      private bool CheckIfContainsKey(ItemBehaviour item)
      {
         for(int i = 0; i < Slots.Count; i++)
         {
            if (Slots.ElementAt(i).Value.item == item)
            {
               return true;
            }
         }
         return false;
      }
   }
}