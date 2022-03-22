using _Scripts.Systems.Lab.Recipes;

namespace _Scripts.Systems.Lab.Machines
{
    public struct MachineStoreValues
    {
        public RecipeObj CurrentRecipeObj;
        public float Time;

        public MachineStoreValues(RecipeObj obj, float time)
        {
            Time = time;
            CurrentRecipeObj = obj;
        }
    }
}