using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Item/Plant")]
    public class PlantBase : ItemBehaviour
    {
        public bool isDried;
        [Header("Plant Stuff")] 
        [EnumFlagsAttribute]
        public MedicalSymptoms MedicalSymptoms;

        public Sprite DriedPlant;

        public GameObject DryingPlant;
    }
}
