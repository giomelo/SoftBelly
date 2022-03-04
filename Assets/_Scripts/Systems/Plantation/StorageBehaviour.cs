using System.Collections.Generic;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Systems.Plantation
{
   public abstract class StorageBehaviour
   {
      private readonly Dictionary<string, int> _slots;

      protected StorageBehaviour(Dictionary<string, int> slots)
      {
         _slots = slots;
      }

      public void AddItem(string key, int amount)
      {
         if (_slots.ContainsKey(key))
         {
            _slots[key] += amount;
         }
         else
         {
            _slots.Add(key, amount);
         }
      }

      public void RemoveItem(string key, int amount)
      {
         _slots[key] -= amount;
         if (_slots[key] <= 0)
         {
            _slots.Remove(key);
         }
      }

      public void Display()
      {
         foreach (var (key, value) in _slots)
         {
            Debug.Log(key);
            Debug.Log(value);

         }
      }
   }
}