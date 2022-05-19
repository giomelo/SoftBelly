using System;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    [Serializable]
    public struct IngredientsList
    {
        public IngredientToppingSO Ingredient;
        public int Amount;

        public IngredientsList(IngredientToppingSO obj, int amount)
        {
            Ingredient = obj;
            Amount = amount;
        }
    }
}