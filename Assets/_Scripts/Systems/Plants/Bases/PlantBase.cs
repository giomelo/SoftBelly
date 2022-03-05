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
        [Header("Plant Stuff")]

        public PlantState PlantState =  PlantState.Seed;

        public GameObject[] PlantDisplayObjs = new GameObject[3];

        #endregion

        #region Methods
        
        // public PlantBase(string id, ItemType itemType) : base(id, itemType)
        // {
        //     ItemId = id;
        //     ItemType = itemType;
        // }
        #endregion


    }

}
