using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    
    /// <summary>
    /// Class tha holds the current storage
    /// </summary>
    
    public class StorageHolder : MonoBehaviour
    {
        public StorageBehaviour Storage;

        //This is for exposing the dictionary in the inspector(unity dont have serialized dictionaries)
        [SerializeField] 
        private List<ExposedInventory> exposedInventory = new();
        private void Start()
        {
            // Dictionary<string, int> slots = new Dictionary<string, int>(PlantationInventory.Size);
            // StorageBehaviour storage = new PlantationInventory(slots);
            // PlantBase plant1 = ScriptableObject.CreateInstance<PlantBase>();
            // plant1.Init("oi");
            InitInventory();
            Storage.Display();
        }
        
        /// <summary>
        /// Method for transform the exposed inventory to the original dictionary inventory
        /// </summary>
        private void InitInventory()
        {
            for (int i = 0; i < exposedInventory.Count; i++)
            {
                Storage.AddItem(exposedInventory[i].item.amount, exposedInventory[i].item.item);
            }
        }



        public void UpdateExposedInventory()
        {
            exposedInventory.Clear();
            for (int i = 0; i < Storage.Slots.Count; i++)
            {
                ExposedInventory aux = new ExposedInventory();
                aux.key =  Storage.Slots.ElementAt(i).Key;
                ItemObj item = new ItemObj(Storage.Slots.ElementAt(i).Value.item,
                    Storage.Slots.ElementAt(i).Value.amount);
                aux.item = item;
                exposedInventory.Add(aux);
            }
        }
    }
    
   
}
