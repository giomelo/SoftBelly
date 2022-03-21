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

        public bool CheckRecipe(RecipeObj r)
        {
            foreach (var recipe in Recipes)
            {
                if (recipe.Ingredients.All(r.Ingredients.Contains) && recipe.Ingredients.Count == r.Ingredients.Count)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
