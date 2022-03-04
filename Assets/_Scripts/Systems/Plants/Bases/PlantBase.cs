using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Plant")]
    public class PlantBase : ItemBehaviour
    {
        #region Fields

        public PlantState PlantState { get; protected set; } =  PlantState.Seed;

        public GameObject[] PlantDisplayObjs = new GameObject[3];

        #endregion

        #region Methods

        #endregion


        public PlantBase(string id) : base(id)
        {
            ItemId = id;
        }
    }

}
