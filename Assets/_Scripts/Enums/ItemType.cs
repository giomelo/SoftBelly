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
        Misturada = 64,
        Cozida = 128,
        Seca = 256,
        Amassada = 512
    }
}