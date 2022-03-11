using _Scripts.Systems.Item;

namespace _Scripts.Systems.Plants.Bases
{
    public struct PlantPlot
    {
        public ItemBehaviour Plant;
        public float Time;

        public PlantPlot(ItemBehaviour itemBehaviour, float time)
        {
            Plant = itemBehaviour;
            Time = time;
        }
    }
}