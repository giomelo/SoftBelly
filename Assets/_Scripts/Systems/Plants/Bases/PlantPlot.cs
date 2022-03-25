using _Scripts.Systems.Item;

namespace _Scripts.Systems.Plants.Bases
{
    public struct PlantPlot
    {
        public ItemBehaviour Plant;
        public float Time;
        public float ThristTime;

        public PlantPlot(ItemBehaviour itemBehaviour, float time, float thristTime)
        {
            Plant = itemBehaviour;
            Time = time;
            ThristTime = thristTime;
        }
    }
}