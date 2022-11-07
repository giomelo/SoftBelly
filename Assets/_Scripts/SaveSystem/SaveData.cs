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
        public abstract void DataToScript();
    }

    public interface DataObject
    {
        public void Load();

        public void Save();
    }
    [Serializable]
    public class SaveHudVariables : SaveData
    {
        public float Money { get; private set; }
        public int Reputation { get; private set; }
        
        public SaveHudVariables(float money, int reputation)
        {
            Money = money;
            Reputation = reputation;
        }
        
        public override void DataToScript()
        {
            UniversalVariables.Instance.Money = Money;
            UniversalVariables.Instance.Reputation = Reputation;
        }
    }
}
