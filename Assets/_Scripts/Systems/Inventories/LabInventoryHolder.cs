using _Scripts.Singleton;
using System;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    public class LabInventoryHolder : StorageHolder
    {
        public static LabInventoryHolder Instance;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
}