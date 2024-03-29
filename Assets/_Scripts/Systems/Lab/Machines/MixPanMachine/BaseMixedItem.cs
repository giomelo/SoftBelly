﻿using System;
using System.Collections.Generic;
using _Scripts.Enums;
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
        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, List<IngredientsList> list, PlantBase plant)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            BasePlant = plant;
            IngredientsList = list;
        }
    }
}