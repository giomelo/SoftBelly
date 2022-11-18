using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    [CreateAssetMenu(fileName = "MixedItem", menuName = "Item/BaseMixed")]
    public class BaseMixedItem : ItemBehaviour
    {
        [Header("Plant Stuff")] 
        public List<IngredientsList> IngredientsList = new List<IngredientsList>();
        public PlantBase BasePlant;
        public void Init(string id, ItemType itemType, Sprite sprite, float price, string itemDescription, List<IngredientsList> list, PlantBase plant)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprietiesDescription = itemDescription;
            BasePlant = plant;
            IngredientsList = list;
        }

        public override void Initialized()
        {
            BasePlantMirror plantBase = new BasePlantMirror(BasePlant);
            BaseMixedItemMirror p = new BaseMixedItemMirror(ItemId, ItemType, ImageDisplay, Price,
                ItemProprietiesDescription, IngredientsList, plantBase);
            
            AllScriptableObjecst.Instance.AddInLisit(p);
        }
    }
}