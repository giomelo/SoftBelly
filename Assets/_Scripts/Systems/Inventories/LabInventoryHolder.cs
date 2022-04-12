using _Scripts.Singleton;
using System;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    public class LabInventoryHolder : StorageHolder , IDontDestroyOnLoad<LabInventoryHolder>
    {
        public static LabInventoryHolder Instance;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}