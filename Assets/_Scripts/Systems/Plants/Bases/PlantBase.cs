using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    /// <summary>
    /// Base class for creating plants as scriptables objects
    /// </summary>
    [CreateAssetMenu(fileName = "Plant", menuName = "Plant")]
    public class PlantBase : ScriptableObject
    {
        #region Fields

        public PlantState PlantState { get; protected set; } =  PlantState.Seed;

        private int _plantId;

        public GameObject[] PlantDisplayObjs = new GameObject[3];

        #endregion

        #region Methods



        #endregion
    }

}
