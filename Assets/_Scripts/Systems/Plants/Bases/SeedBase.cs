using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Item/Plant")]
    public class SeedBase: ItemBehaviour
    {
        #region Fields
        [field: Header("Plant Stuff")]

        [Tooltip("First object is the ground with seed, second is the pant growing, third is the plant full grown up")]
        public GameObject[] PlantDisplayObjs = new GameObject[3];
        
        public string ScientificName;
        [TextArea] 
        public string PlantProprieties;
        
        [Header("Time needed to full grow the seed")]
        [Tooltip("In seconds")]
        public float GrowTime;

        #endregion
    }

}
