using System;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Systems.Item
{
    /// <summary>
    /// Base item class behavior for all items
    /// </summary>
    public abstract class ItemBehaviour : ScriptableObject
    {
        [Header("Item Stuff")]
        public string ItemId;
        [EnumFlags]
        public ItemType ItemType;

        public Sprite ImageDisplay;

        public float Price;

        public GameObject ItemProprietiesGO;

        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprietiesGO = itemProprietiesGo;
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
