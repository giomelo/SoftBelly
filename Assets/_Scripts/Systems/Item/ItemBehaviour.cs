using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Systems.Item
{
    [Serializable]
    public struct ItemProprietiesInspector
    {
        public GameObject ItemProprietiesGO;
        [TextArea]
        public string ItemProprietiesDescription;
    }
    
    /// <summary>
    /// Base item class behavior for all items
    /// </summary>
    public abstract class ItemBehaviour : ScriptableObject
    {
        [Header("Item Stuff")] public string ItemId = "";
        [EnumFlags]
        public ItemType ItemType;

        public Sprite ImageDisplay;

        public float Price;

        public ItemProprietiesInspector ItemProprieties;

        public virtual void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
        }

        public bool Equals(ItemBehaviour other)
        {
            return ItemId == other.ItemId;
        }

        // public override bool Equals(object other)
        // {
        //     var aux = other as ItemBehaviour;
        //     return ItemId == aux.ItemId;
        // }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ItemId);
        }
    }
}
