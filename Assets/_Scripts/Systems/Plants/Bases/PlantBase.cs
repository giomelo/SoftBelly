using System;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    [Serializable]
    public struct DriedPlant
    {
        public Sprite DriedPlantImage;

        public GameObject DryingPlant;
    }
    
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Item/Plant")]
    public class PlantBase : ItemBehaviour
    {
        public bool isDried;
        [Header("Plant Stuff")] 
        [EnumFlags]
        public MedicalSymptoms MedicalSymptoms;
        //public DriedPlant DriedPlant;
        public Sprite DriedPlantImage;

        public GameObject DryingPlant;
    }
}
