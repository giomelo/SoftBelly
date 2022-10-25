using System;

namespace _Scripts.Enums
{
    /// <summary>
    /// Define item type
    /// </summary>
    [Flags]
    public enum ItemType
    {
        Seed = 1,
        Plant = 2,
        Potion = 4,
        Ingredient = 8,
        Other = 16,
        Garbage = 32,
        MixedPlant = 64,
        Burned = 128,
        Dryed = 256,
        Smashed = 512
    }
}