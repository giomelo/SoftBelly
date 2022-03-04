using System;
using System.Collections.Generic;
using _Scripts.Editor.FlagsAtributeEditor;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plantation
{
    /// <summary>
    /// Scriptable object tha has the StorageBehavior
    /// </summary>
    [CreateAssetMenu(fileName = "PlantInventory", menuName = "Inventories/PlantInventory")]
    public class PlantationInventory : StorageBehaviour
    {
        [SerializeField]
        protected int Width = 4;
        [SerializeField]
        protected int Height = 4;
        [EnumFlagsAtribute]
        public ItemType itensType;
        public PlantationInventory()
        {
            Slots = new Dictionary<ItemBehaviour, int>(Width * Height);
        }
    }
}
