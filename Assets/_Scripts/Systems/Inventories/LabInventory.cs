using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plantation;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    /// <summary>
    /// Scriptable object that has the StorageBehavior
    /// </summary>
    [CreateAssetMenu(fileName = "LabInventory", menuName = "Inventories/LabInventory")]
    public class LabInventory : StorageBehaviour
    {
        private static LabInventory _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }
            _instance = this;
        }
        public LabInventory()
        {
            Slots = new Dictionary<int, ItemObj>(Size);
        }
    }
}