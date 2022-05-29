using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    [Serializable]
    public struct DriedPlant
    {
        public Sprite DriedPlantImage;

        public GameObject DryingPlant;
    }

    [Serializable]
    public struct SmashedPlant
    {
        public Sprite SmashedPlantImage;
        public GameObject SmashedPlantObj;
    }
    [Serializable]
    public struct MixedPlant
    {
        public Sprite MixedPlantImage;
        public GameObject MixedPlantObject;
    }
    
    [Serializable]
    public struct BurnedPlant
    {
        public Sprite BurnedPlantImage;
    }

    [Serializable]
    public struct PotionStuff
    {
        public Sprite PotionSprite;
    }
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Item/Plant")]
    public class PlantBase : ItemBehaviour
    {
        [Header("Plant Stuff")] 
        [EnumFlags]
        public MedicalSymptoms MedicalSymptoms;
        public DriedPlant DriedPlant;
        public SmashedPlant SmashedPlant;
        public MixedPlant MixedPlant;
        public PotionStuff PotionStuff;
        public BurnedPlant BurnedPlant;
        [HideInInspector] public List<MachinesTypes> MachineList = new List<MachinesTypes>();


        private void OnEnable()
        {
            MachineList.Clear();
        }

        public void AddMachine(MachinesTypes type)
        {
            if (type == MachinesTypes.Nothing) return;
            MachineList.Add(type);
        }
        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, BurnedPlant burnedPlant, MixedPlant mixedPlant, DriedPlant driedPlant, PotionStuff potion, SmashedPlant smashedPlant, MedicalSymptoms medicalSymptoms)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            PotionStuff = potion;
            DriedPlant = driedPlant;
            SmashedPlant = smashedPlant;
            MixedPlant = mixedPlant;
            BurnedPlant = burnedPlant;
            MedicalSymptoms = medicalSymptoms;
            //MachineList = machineTypes;
        }
    }
}
