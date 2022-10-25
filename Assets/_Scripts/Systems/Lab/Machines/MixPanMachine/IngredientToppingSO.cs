using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    [CreateAssetMenu(fileName = "IngredientTopping", menuName = "Ingredient/IngredientToppping")]
    public class IngredientToppingSO : ScriptableObject
    {
        public string IngredientDescription;
    }
}