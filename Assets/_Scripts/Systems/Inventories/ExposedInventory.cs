using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Systems.Item;

namespace _Scripts.Systems.Inventories
{
    /// <summary>
    /// Base struct for show the inventory in unity inspector
    /// </summary>
    [Serializable]
    public struct ExposedInventory
    {
        public ItemBehaviour item;
        public int amount;
       
    }
}