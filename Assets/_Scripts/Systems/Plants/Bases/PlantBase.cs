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
        #region Fields
        [field: Header("Plant Stuff")]
        public PlantState PlantState { get; }  = PlantState.Seed;
        
        public GameObject[] PlantDisplayObjs = new GameObject[3];
        
        public string ScientificName;
        [TextArea] 
        public string PlantProprieties;
        #endregion
    }

}
