using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Store
{
    public class StoreController : MonoSingleton<StoreController>
    {
        private StorageHolder StorageHolder;

        private void Start()
        {
            var storagesInScene = GameObject.FindObjectsOfType<StorageHolder>();
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            foreach (StorageHolder s in storagesInScene)
            {
                if (s.Storage.InventoryType == InventoryType.Plantation)
                {
                    StorageHolder = s;
                }
            }
        }

        public void AddItem(ItemBehaviour item)
        {
            StorageHolder.Storage.AddItem(1,item);
        }
    }
}