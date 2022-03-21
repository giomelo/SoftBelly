using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Systems.Lab.Recipes
{
    /// <summary>
    /// Base class for creating recipes
    /// </summary>
    [CreateAssetMenu(fileName = "Recipe")]
    public class RecipeObj : ScriptableObject
    {
        public List<MachineSlot> Ingredients = new List<MachineSlot>();
        public List<MachineSlot> Results = new List<MachineSlot>();

        public RecipeObj(List<MachineSlot> ingredients)
        {
            Ingredients = ingredients;
        }
        
        public RecipeObj(List<MachineSlot> ingredients, List<MachineSlot> results)
        {
            Ingredients = ingredients;
            Results = results;
        }

        public void Init(List<MachineSlot> ingredients)
        {
            Ingredients = ingredients;
        }
    }
}