using System;
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
        public int Nivel { get; private set; }
        
        public SaveHudVariables(float money, int reputation, float socialAlignment, int nivel)
        {
            Money = money;
            Reputation = reputation;
            SocialAlignment = socialAlignment;
            Nivel = nivel;

        }
    }
}
