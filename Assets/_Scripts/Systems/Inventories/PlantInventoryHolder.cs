using _Scripts.Singleton;

namespace _Scripts.Systems.Inventories
{
    public class PlantInventoryHolder : StorageHolder
    {
        private static PlantInventoryHolder _instance;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
    }
}