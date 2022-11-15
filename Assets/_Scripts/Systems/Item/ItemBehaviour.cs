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
    [Serializable]
    public struct ItensProprietiesMirror
    {
        public string ItemProprietiesGO;
        [TextArea]
        public string ItemProprietiesDescription;
    }


    /// <summary>
    /// Base item class behavior for all items
    /// </summary>
    public class ItemBehaviour : ScriptableObject
    {
        [Header("Item Stuff")] public string ItemId = "";

        public string ItemLongID = "";
        [EnumFlags]
        public ItemType ItemType;

        public Sprite ImageDisplay;

        public float Price;

        [TextArea]
        public string ShopDescription;

        public ItemProprietiesInspector ItemProprieties;

        public virtual void Initialized(){}

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

        public bool Equals(string s)
        {
            return ItemId == s;
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
