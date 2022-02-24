using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;


namespace Systems.Plants.Bases
{
    [CreateAssetMenu(fileName = "Plant", menuName = "Plant")]
    public class PlantBase : ScriptableObject
    {
        #region Fields
        public string PlantName;

        public PlantState PlantState = PlantState.Seed;
        #endregion
    }

}
