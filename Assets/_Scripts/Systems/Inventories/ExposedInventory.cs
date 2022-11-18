using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.Systems.Plants.Bases;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    /// <summary>
    /// Base struct for show the inventory in unity inspector
    /// </summary>
    [Serializable]
    public struct ExposedInventory
    {
        [HideInInspector]
        public int key;
        public ItemObj item;
    }

    [Serializable]
    public class SmashedPlantMirror : BaseMirrorItem
    {
        public BasePlantMirror BasePlant;

        public SmashedPlantMirror(string id, ItemType itemType, Sprite sprite, float price, string itemDescription, BasePlantMirror plant)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = AllScriptableObjecst.Instance.sprites.FindIndex(sprite);
            Price = price;
            ItemProprietiesDescription = itemDescription;
            BasePlant = plant;
        }
    }
    
    [Serializable]
    public class PotionMirror : BaseMirrorItem
    {
        public List<SymptomsNivel> Cure = new List<SymptomsNivel>();
        public PotionType PotionType;

        public PotionMirror(PotionBase potion)
        {
            ItemId = potion.ItemId;
            ItemType = potion.ItemType;
            ImageDisplay =  AllScriptableObjecst.Instance.sprites.FindIndex(potion.ImageDisplay);
            Price = potion.Price;
            ItemProprietiesDescription = potion.ItemProprietiesDescription;
            Cure = potion.Cure;
            PotionType = potion.PotionType;
        }
    }
    
    [Serializable]
    public class BaseMixedItemMirror : BaseMirrorItem
    {
        public List<IngredientsList> IngredientsList = new List<IngredientsList>();
        public BasePlantMirror BasePlant;

        public BaseMixedItemMirror(string id, ItemType itemType, Sprite sprite, float price, string itemDescription, List<IngredientsList> list, BasePlantMirror plant)
        {
            
            ItemId = id;
            ItemType = itemType;
            ImageDisplay =  AllScriptableObjecst.Instance.sprites.FindIndex(sprite);
            Price = price;
            ItemProprietiesDescription = itemDescription;
            BasePlant = plant;
            IngredientsList = list;
        }
    }

    [Serializable]
    public class SeedBaseMirror : BaseMirrorItem
    {
        [Header("Time needed to full grow the seed")]
        [Tooltip("In seconds")]
        public float GrowTime;
        public float WaterCicles = 2;
        [Header("Grow up seed")]
        public BasePlantMirror PlantBase;

        public SeedBaseMirror(string id, ItemType itemType, Sprite sprite, float price, string itemDescription, float grow, float water,BasePlantMirror plantBase)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = AllScriptableObjecst.Instance.sprites.FindIndex(sprite);
            Price = price;
            ItemProprietiesDescription = itemDescription;

            GrowTime = grow;
            WaterCicles = water;
            PlantBase = plantBase;
        }
    }
    [Serializable]
    public class BasePlantMirror : BaseMirrorItem
    {
        public List<SymptomsNivel> MedicalSymptoms;
        public int DriedPlant;
        public int SmashedPlant;
        public int MixedPlant;
        public int PotionStuff;
        public int BurnedPlant;
        public List<MachinesTypes> MachineList = new List<MachinesTypes>();
        public PlantTemperature plantTemperature;

        public PlantBase MirrorToBase()
        {
            var p = ScriptableObject.CreateInstance<PlantBase>();
            p.Init(ItemId, ItemType, AllScriptableObjecst.Instance.sprites.FindSprite(ImageDisplay), Price, ItemProprietiesDescription,
                AllScriptableObjecst.Instance.sprites.FindSprite(BurnedPlant), AllScriptableObjecst.Instance.sprites.FindSprite(MixedPlant), AllScriptableObjecst.Instance.sprites.FindSprite(DriedPlant), AllScriptableObjecst.Instance.sprites.FindSprite(PotionStuff),AllScriptableObjecst.Instance.sprites.FindSprite(SmashedPlant), MedicalSymptoms, plantTemperature, MachineList);
            return p;
        }

        public BasePlantMirror(PlantBase plantBase)
        {
            ItemId = plantBase.ItemId;
            ItemType = plantBase.ItemType;
            ImageDisplay = AllScriptableObjecst.Instance.sprites.FindIndex(plantBase.ImageDisplay);
            Price = plantBase.Price;
            ItemProprietiesDescription = plantBase.ItemProprietiesDescription;
            PotionStuff =AllScriptableObjecst.Instance.sprites.FindIndex(plantBase.PotionStuff);
            DriedPlant = AllScriptableObjecst.Instance.sprites.FindIndex(plantBase.DriedPlant);
            SmashedPlant = AllScriptableObjecst.Instance.sprites.FindIndex(plantBase.SmashedPlant);
            MixedPlant = AllScriptableObjecst.Instance.sprites.FindIndex(plantBase.MixedPlant);
            BurnedPlant = AllScriptableObjecst.Instance.sprites.FindIndex(plantBase.BurnedPlant);
            MedicalSymptoms = plantBase.MedicalSymptoms;
        }
    }
    
    [Serializable]
    public class BaseMirrorItem
    {
        
        public string ItemId = "";

        public string ItemLongID = "";
        [EnumFlags]
        public ItemType ItemType;

        public int ImageDisplay;

        public float Price;

        [TextArea]
        public string ShopDescription;

        public string ItemProprietiesDescription;
        
        public BaseMirrorItem(string id, ItemType itemType, Sprite sprite, float price, string itemDescription)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = AllScriptableObjecst.Instance.sprites.FindIndex(sprite);
            Price = price;
            ItemProprietiesDescription= itemDescription;
        }

        public BaseMirrorItem()
        {
            
        }
        
    }
    
    [Serializable]
    public struct MirrorItem
    {
        public int Amount;
        public BaseMirrorItem Name;

        public MirrorItem(int amout, BaseMirrorItem name)
        {
            Name = name;
            Amount = amout;
        }
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

        public void ItemBehaviourToMirror()
        {
            
        }
        
        
    }
}