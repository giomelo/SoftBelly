using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.PestleMachine
{
    [CreateAssetMenu(fileName = "MixedItem", menuName = "Item/MixedItem")]
    public class SmashedItemBase : ItemBehaviour
    {
        public PlantBase BasePlant;
        
        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, PlantBase plant)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            BasePlant = plant;
        }

        public override void Initialized()
        {
            BasePlantMirror plantBase = new BasePlantMirror(BasePlant);
            SmashedPlantMirror p = new SmashedPlantMirror(ItemId, ItemType, ImageDisplay, Price, ItemProprieties.ItemProprietiesGO,
                ItemProprieties.ItemProprietiesDescription, plantBase);
            AllScriptableObjecst.Instance.AddInLisit(p);
        }
    }
}
