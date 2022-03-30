using System;

namespace _Scripts.Enums
{
    /// <summary>
    /// Define item type
    /// </summary>
    [Flags]
    public enum ItemType
    {
        Seed,
        Plant,
        Potion,
        Ingredient,
        Other,
        Garbage
    }
}