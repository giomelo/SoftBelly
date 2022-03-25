using System;
using System.Collections.Generic;
using _Scripts.Systems.Inventories;
using UnityEngine;

namespace _Scripts.Systems.Lab.Recipes
{
    /// <summary>
    /// Base class for creating recipes
    /// </summary>
    [CreateAssetMenu(fileName = "Recipe")]
    public class RecipeObj : ScriptableObject
    {
        public List<ItemObj> Ingredients = new List<ItemObj>();
        public List<ItemObj> Results = new List<ItemObj>();

        public RecipeObj(List<ItemObj> ingredients)
        {
            Ingredients = ingredients;
        }
        
        public RecipeObj(List<ItemObj> ingredients, List<ItemObj> results)
        {
            Ingredients = ingredients;
            Results = results;
        }

        public void Init(List<ItemObj> ingredients)
        {
            Ingredients = ingredients;
        }
    }
}