using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;
using System;
using _Scripts.SaveSystem;
using _Scripts.Systems.Lab;

namespace _Scripts.Systems.Inventories
{
   /// <summary>
   /// Behavior for all types of storage, handles the main methods like addItens, removeItens, searchItens
   /// </summary>
   public abstract class StorageBehaviour : ScriptableObject
   {
      public Dictionary<int, ItemObj> Slots;
      private Dictionary<int, MirrorItem> SlotsToSave;
      public int maxAmountPerSlots = 20;
      [EnumFlags]
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
         var index = 0;
         if (CheckIfContainsKey(item))
         {
            index = CheckIfSlotAreAvailabe(item);
         }
         else
         { 
            index = ReturnFirstEmptySlot();
         }
         Put(index, item, amount);
         item.Initialized();
         Display();
      }

      private void Put(int index, ItemBehaviour item, int amount)
      {
         if (index == Slots.Count)
         {
            Slots.Add(index, new ItemObj(item, amount));
            return;
         }
         var auxObj = Slots[index];
         auxObj.amount += amount;
         auxObj.item = item;
         Slots[index] = auxObj;   
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

      private int ReturnFirstEmptySlot()
      {
         for(int i = 0; i < Slots.Count; i++)
         {
            var amountInSlot = Slots.ElementAt(i).Value.amount;
            if (amountInSlot <= 0)
            {
               return i;
            }
         }
         return Slots.Count;
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
         for(int i = 0; i < Slots.Count; i++)
         {
            if (!Slots.ElementAt(i).Value.item.Equals(item)) continue;
            if (first)
            {
               last = Slots.ElementAt(i).Value.amount;
               index = i;
               first = false;
            }

            if (Slots.ElementAt(i).Value.amount >= last) continue;
            last = Slots.ElementAt(i).Value.amount;
            index = i;
         }
         
         var auxObj = Slots.ElementAt(index).Value;
         auxObj.amount -= 1;

         if (auxObj.amount <= 0)
         {
            auxObj.item = null;
         }
         Slots[index] = auxObj;
      }

      public void Display()
      {
         foreach (var (key, value) in Slots)
         {
            Debug.Log("Item: " + value.item.ItemId);
            Debug.Log("Amount: " + value.amount);

         }
      }
      //Chek if there is space in the slot, else create new slot
      public int CheckIfSlotAreAvailabe(ItemBehaviour item)
      {
         for(int i = 0; i < Slots.Count; i++)
         {
            var amountInSlot = Slots.ElementAt(i).Value.amount;
            if (!Slots.ElementAt(i).Value.item.Equals(item)) continue;
            
            if (amountInSlot < maxAmountPerSlots)
            {
               return i;
            }
         }
         return Slots.Count;
      }

      public PotionBase CheckIfContainsKey(MedicalSymptoms item, PotionType type)
      {
         PotionBase potion = null;
         for(int i = 0; i < Slots.Count; i++)
         {
            //ser o item comparando nao eh uma poção
            if (!(Slots.ElementAt(i).Value.item as PotionBase)) continue;
            PotionBase auxInventory = Slots.ElementAt(i).Value.item as PotionBase;
            if (!auxInventory.CheckIfPortionHasCure(item)) continue;
            if (!auxInventory.CheckIfPotionIsType(type)) continue;
            potion = auxInventory;
         }

         return potion;
      }

      public bool CheckIfContainsKey(ItemBehaviour item)
      {
         for(int i = 0; i < Slots.Count; i++)
         {
            if (Slots.ElementAt(i).Value.item == null) continue;
            if (Slots.ElementAt(i).Value.item.Equals(item))
            {
               return true;
            }
         }

         return false;
      }

      public void Clear()
      {
         Slots.Clear();
      }
      private int GenerateKey()
      {
         return Slots.Count;
      }


      private void SetList()
      {
         SlotsToSave.Clear();

         foreach (var slot in Slots)
         {
            BaseMirrorItem b = new BaseMirrorItem(slot.Value.item.ItemId, slot.Value.item.ItemType, slot.Value.item.ImageDisplay, slot.Value.item.Price, slot.Value.item.ItemProprietiesDescription);
            MirrorItem m = new MirrorItem(slot.Value.amount, b);
            SlotsToSave.Add(slot.Key, m);
         }
      }
   }
}