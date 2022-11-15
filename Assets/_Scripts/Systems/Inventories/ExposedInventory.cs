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

        public SmashedPlantMirror(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, BasePlantMirror plant)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = AssetDatabase.GetAssetPath(sprite);
            Price = price;
            ItemProprieties.ItemProprietiesGO = AssetDatabase.GetAssetPath(itemProprietiesGo);
            ItemProprieties.ItemProprietiesDescription = itemDescription;
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
            ImageDisplay =  AssetDatabase.GetAssetPath(potion.ImageDisplay);
            Price = potion.Price;
            ItemProprieties.ItemProprietiesGO = AssetDatabase.GetAssetPath( potion.ItemProprieties.ItemProprietiesGO);
            ItemProprieties.ItemProprietiesDescription = potion.ItemProprieties.ItemProprietiesDescription;
            Cure = potion.Cure;
            PotionType = potion.PotionType;
        }
    }
    
    [Serializable]
    public class BaseMixedItemMirror : BaseMirrorItem
    {
        public List<IngredientsList> IngredientsList = new List<IngredientsList>();
        public BasePlantMirror BasePlant;

        public BaseMixedItemMirror(string id, ItemType itemType, Sprite sprite, float price,
            GameObject itemProprietiesGo, string itemDescription, List<IngredientsList> list, BasePlantMirror plant)
        {
            
            ItemId = id;
            ItemType = itemType;
            ImageDisplay =  AssetDatabase.GetAssetPath(sprite);
            Price = price;
            ItemProprieties.ItemProprietiesGO = AssetDatabase.GetAssetPath(itemProprietiesGo);
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            BasePlant = plant;
            IngredientsList = list;
        }
    }

    [Serializable]
    public class SeedBaseMirror : BaseMirrorItem
    {
        public string[] PlantDisplayObjs = new string[3];

        [Header("Time needed to full grow the seed")]
        [Tooltip("In seconds")]
        public float GrowTime;
        public float WaterCicles = 2;
        [Header("Grow up seed")]
        public BasePlantMirror PlantBase;

        public SeedBaseMirror(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, GameObject[] plants, float grow, float water,BasePlantMirror plantBase)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = AssetDatabase.GetAssetPath(sprite);
            Price = price;
            ItemProprieties.ItemProprietiesGO = AssetDatabase.GetAssetPath(itemProprietiesGo);
            ItemProprieties.ItemProprietiesDescription = itemDescription;

            for (int i =0; i< plants.Length; i++)
            {
                PlantDisplayObjs[i] = AssetDatabase.GetAssetPath(plants[i]) ;
            }
            
            GrowTime = grow;
            WaterCicles = water;
            PlantBase = plantBase;
        }
    }
    [Serializable]
    public class BasePlantMirror : BaseMirrorItem
    {
        public List<SymptomsNivel> MedicalSymptoms;
        public string DriedPlant;
        public string SmashedPlant;
        public string MixedPlant;
        public string PotionStuff;
        public string BurnedPlant;
        public List<MachinesTypes> MachineList = new List<MachinesTypes>();
        public PlantTemperature plantTemperature;

        public PlantBase MirrorToBase()
        {
            var p = ScriptableObject.CreateInstance<PlantBase>();
            p.Init(ItemId, ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(ImageDisplay, typeof(Sprite)), Price, (GameObject)AssetDatabase.LoadAssetAtPath(ItemProprieties.ItemProprietiesGO, typeof(GameObject)), ItemProprieties.ItemProprietiesDescription,
                (Sprite)AssetDatabase.LoadAssetAtPath(BurnedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(MixedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(DriedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(PotionStuff, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(SmashedPlant, typeof(Sprite)), MedicalSymptoms, plantTemperature, MachineList);
            return p;
        }

        public BasePlantMirror(PlantBase plantBase)
        {
            ItemId = plantBase.ItemId;
            ItemType = plantBase.ItemType;
            ImageDisplay = AssetDatabase.GetAssetPath(plantBase.ImageDisplay);
            Price = plantBase.Price;
            ItemProprieties.ItemProprietiesGO = AssetDatabase.GetAssetPath(plantBase.ItemProprieties.ItemProprietiesGO);
            ItemProprieties.ItemProprietiesDescription = plantBase.ItemProprieties.ItemProprietiesDescription;
            PotionStuff = AssetDatabase.GetAssetPath(plantBase.PotionStuff);
            DriedPlant = AssetDatabase.GetAssetPath(plantBase.DriedPlant);
            SmashedPlant = AssetDatabase.GetAssetPath(plantBase.SmashedPlant);
            MixedPlant = AssetDatabase.GetAssetPath(plantBase.MixedPlant);
            BurnedPlant = AssetDatabase.GetAssetPath(plantBase.BurnedPlant);
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

        public string ImageDisplay;

        public float Price;

        [TextArea]
        public string ShopDescription;

        public ItensProprietiesMirror ItemProprieties;
        
        public BaseMirrorItem(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = AssetDatabase.GetAssetPath(sprite);
            Price = price;
            ItemProprieties.ItemProprietiesGO = AssetDatabase.GetAssetPath(itemProprietiesGo);
            ItemProprieties.ItemProprietiesDescription = itemDescription;
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