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
        public PlantationInventory()
        {
            Slots = new Dictionary<ItemBehaviour, int>(Width * Height);
        }
    }
}
