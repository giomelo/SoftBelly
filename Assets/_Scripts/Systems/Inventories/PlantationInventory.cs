using System;
using System.Collections.Generic;
using _Scripts.Editor.FlagsAtributeEditor;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plantation;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    /// <summary>
    /// Scriptable object that has the StorageBehavior
    /// </summary>
    [CreateAssetMenu(fileName = "PlantInventory", menuName = "Inventories/PlantInventory")]
    public class PlantationInventory : StorageBehaviour
    {
        private static PlantationInventory Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
        public PlantationInventory()
        {
            Slots = new Dictionary<int, ItemObj>(Size);
        }
        
    }
    
    
}
