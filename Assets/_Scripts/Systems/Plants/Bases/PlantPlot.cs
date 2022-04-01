using System;
using _Scripts.Systems.Item;

namespace _Scripts.Systems.Plants.Bases
{
    [Serializable]
    public struct PlantPlot
    {
        public SeedBase Plant;
        public float Time;
        public float ThristTime;
        public bool IsThirsty;

        public PlantPlot(SeedBase itemBehaviour, float time, float thristTime, bool isThirsty)
        {
            Plant = itemBehaviour;
            Time = time;
            ThristTime = thristTime;
            IsThirsty = isThirsty;

        }
    }
}