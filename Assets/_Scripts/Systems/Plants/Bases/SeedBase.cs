using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Item/Seed")]
    public class SeedBase: ItemBehaviour
    {
        #region Fields
        [field: Header("Plant Stuff")]

        [Tooltip("First object is the ground with seed, second is the pant growing, third is the plant full grown up")]
        public GameObject[] PlantDisplayObjs = new GameObject[3];

        [Header("Time needed to full grow the seed")]
        [Tooltip("In seconds")]
        public float GrowTime;
        public float WaterCicles = 2;
        [Header("Grow up seed")]
        public PlantBase PlantBase;
        
        
        #endregion


        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, GameObject[] plants, float grow, float water,PlantBase plantBase )
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            PlantDisplayObjs = plants;
            GrowTime = grow;
            WaterCicles = water;
            PlantBase = plantBase;
        }

        public override void Initialized()
        {
            BasePlantMirror plantBase = new BasePlantMirror(PlantBase);
            SeedBaseMirror p = new SeedBaseMirror(ItemId, ItemType, ImageDisplay, Price, ItemProprieties.ItemProprietiesGO,
                ItemProprieties.ItemProprietiesDescription, PlantDisplayObjs, GrowTime, WaterCicles, plantBase);
            AllScriptableObjecst.Instance.AddInLisit(p);
        }
    }

}
