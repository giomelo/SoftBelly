using System;
using System.Collections.Generic;
using _Scripts.Singleton;

namespace _Scripts.Systems.Plantation
{
    [Serializable]
    public struct ExposedDictionary {
        public string name;
        public int value;
    }

    public class PlantationInventory : StorageBehaviour
    {
        public ExposedDictionary[] slots;
        public Dictionary<string, int> Slots = new Dictionary<string, int>(10);
        
        public PlantationInventory(Dictionary<string, int> slots) : base(slots)
        {
            
        }
    }
}
