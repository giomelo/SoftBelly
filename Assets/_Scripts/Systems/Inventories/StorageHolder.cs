using System;
using System.Collections.Generic;
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
            // PlantBase plant2 = ScriptableObject.CreateInstance<PlantBase>();
            // plant2.Init("oi2");
            // Storage.AddItem(plant1, 10);
            // Storage.AddItem(plant2, 11);
            // Storage.RemoveItem(plant1, 9);
            // Storage.AddItem(plant2, 5);
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
                Storage.AddItem(i,exposedInventory[i].item.amount, exposedInventory[i].item.item);
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
   
}
