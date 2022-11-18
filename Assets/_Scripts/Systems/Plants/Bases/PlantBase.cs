using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    [Serializable]
    public struct MirrorPlantsExtras
    {
        private string Go;
        public string Image;

        public MirrorPlantsExtras(string image, string go)
        {
            Go = go;
            Image = image;
        }
    }


    [Serializable]
    public struct DriedPlant
    {
        public Sprite DriedPlantImage;
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
    [Serializable]
    public struct SymptomsNivel
    {
        public MedicalSymptoms Symptoms;
        public SymtomsNivel Nivel;
    }
    [Serializable]
    public enum PlantTemperature
    {
        Low = 0,
        Medium = 1,
        High =2
    }
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Item/Plant")]
    public class PlantBase : ItemBehaviour
    {
        [Header("Plant Stuff")]
        public List<SymptomsNivel> MedicalSymptoms;
        public Sprite DriedPlant;
        public Sprite SmashedPlant;
        public Sprite MixedPlant;
        public Sprite PotionStuff;
        public Sprite BurnedPlant;
        [HideInInspector] public List<MachinesTypes> MachineList = new List<MachinesTypes>();
        public PlantTemperature plantTemperature;

        private void OnEnable()
        {
            MachineList.Clear();
        }

        public void AddMachine(MachinesTypes type)
        {
            if (type == MachinesTypes.Nothing) return;
            MachineList.Add(type);
        }
        public void Init(string id, ItemType itemType, Sprite sprite, float price, string itemDescription, Sprite burnedPlant, Sprite mixedPlant, Sprite driedPlant, Sprite potion, Sprite smashedPlant, List<SymptomsNivel> medicalSymptoms,
            PlantTemperature temperature, List<MachinesTypes> machines)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprietiesDescription = itemDescription;
            PotionStuff = potion;
            DriedPlant = driedPlant;
            SmashedPlant = smashedPlant;
            MixedPlant = mixedPlant;
            BurnedPlant = burnedPlant;
            MedicalSymptoms = medicalSymptoms;
            MachineList = machines;
            plantTemperature = temperature;

        }

        public override void Initialized()
        {
            BasePlantMirror plant = new BasePlantMirror(this);
            AllScriptableObjecst.Instance.AddInLisit(plant);
        }
    }
}
