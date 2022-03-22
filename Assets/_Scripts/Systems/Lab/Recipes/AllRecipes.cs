using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Singleton;

namespace _Scripts.Systems.Lab.Recipes
{
    public class AllRecipes : MonoSingleton<AllRecipes>
    {
        [SerializeField]
        private List<RecipeObj> Recipes = new List<RecipeObj>();
        
        /// <summary>
        /// Return the orignial recipe with the current igredients
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public RecipeObj CheckRecipe(RecipeObj r)
        {
            foreach (var recipe in Recipes)
            {
                if (recipe.Ingredients.All(r.Ingredients.Contains) && recipe.Ingredients.Count == r.Ingredients.Count)
                {
                    return recipe;
                }
            }

            return null;
        }
    }
}
