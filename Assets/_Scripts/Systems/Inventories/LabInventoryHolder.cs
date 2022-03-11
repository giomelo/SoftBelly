using _Scripts.Singleton;
using System;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    public class LabInventoryHolder : StorageHolder , IDontDestroyOnLoad<LabInventoryHolder>
    {
        private static LabInventoryHolder _instance;
        
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