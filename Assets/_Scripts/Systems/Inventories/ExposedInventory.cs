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
        public int key;
        public ItemObj item;
    }
    
    [Serializable]
    public struct ItemObj
    {
        public ItemBehaviour item;
        public int amount;

        public ItemObj(ItemBehaviour itemBehaviour, int i)
        {
            item = itemBehaviour;
            amount = i;
        }
    }
}