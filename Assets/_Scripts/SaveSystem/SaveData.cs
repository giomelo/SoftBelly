using System;
using System.Collections.Generic;
using _Scripts.Systems.Inventories;
using _Scripts.U_Variables;

namespace _Scripts.SaveSystem
{
    [Serializable]
    public abstract class SaveData
    {
        public SaveData()
        {
            
        }
        
        public SaveData(SaveData data)
        {
            
        }
    }

    public interface DataObject
    {
        public void Load();

        public void Save();
    }
    
    [Serializable]
    public class SaveStorage : SaveData
    {
        public Dictionary<int, MirrorItem> Slots { get; private set; }

        public SaveStorage(Dictionary<int, MirrorItem> slots)
        {
            Slots = slots;
        }
    }

    [Serializable]
    public class SaveScript : SaveData
    {
        public List<BaseMirrorItem> Items { get; private set; }


        public SaveScript(List<BaseMirrorItem> items)
        {
            Items = items;
        }
    }
    
    
    [Serializable]
    public class SaveLockedObject : SaveData
    {
        public bool IsLocked { get; private set; }

        public SaveLockedObject(bool locked)
        {
            IsLocked = locked;
        }
    }
    
    [Serializable]
    public class SaveDay : SaveData
    {
        public int Day { get; private set; }
        public SaveDay(int day)
        {
            Day = day;
        }
    }
    
    [Serializable]
    public class SaveHudVariables : SaveData
    {
        public float Money { get; private set; }
        public int Reputation { get; private set; }
        public float SocialAlignment { get; private set; }
        public float Nivel { get; private set; }
        
        public SaveHudVariables(float money, int reputation, float socialAlignment, float nivel)
        {
            Money = money;
            Reputation = reputation;
            SocialAlignment = socialAlignment;
            Nivel = nivel;

        }
    }
}
