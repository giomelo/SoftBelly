using System;

namespace _Scripts.Enums
{
    /// <summary>
    /// Define medical symptoms that can be cured
    /// </summary>
    [Flags]
    public enum MedicalSymptoms
    {
        DorDeCabeça = 1,
        Febre = 2
    }
}