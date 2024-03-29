using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Systems.Item;
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
            InitInventory();
        }
        
        /// <summary>
        /// Method for transform the exposed inventory to the original dictionary inventory
        /// </summary>
        private void InitInventory()
        {
            for (int i = 0; i < exposedInventory.Count; i++)
            {
                Storage.AddItem(exposedInventory[i].item.amount, exposedInventory[i].item.item);
                exposedInventory[i].item.item.Init(exposedInventory[i].item.item.ItemId,exposedInventory[i].item.item.ItemType, exposedInventory[i].item.item.ImageDisplay, exposedInventory[i].item.item.Price, exposedInventory[i].item.item.ItemProprieties.ItemProprietiesGO,exposedInventory[i].item.item.ItemProprieties.ItemProprietiesDescription);
            }
        }
        /// <summary>
        /// Update the exposed inventory in the inspecto
        /// </summary>
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
