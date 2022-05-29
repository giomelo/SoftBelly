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
        Febre = 2,
        Alergia = 4,
        EliminaçãodeToxinas = 8,
        Ansiedade = 16,
        DorNoEstomago = 32,
        DorDeDente = 64,
        Queimaduras = 128,
        Nauseas = 256,
        Insonia = 512,
        Estresse = 1024,
        InfeccaoNaGarganta = 2048,
        Anemia = 4096,
        AntiInflamatorio = 8192,
        DoencasCardiovas = 16384,
        Hemorragia = 32768,
        Glaucoma = 65536,
        Asma = 131072

    }
}