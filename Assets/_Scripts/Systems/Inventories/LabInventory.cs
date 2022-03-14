using System.Collections.Generic;
using _Scripts.Editor.FlagsAtributeEditor;
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
        private static LabInventory Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
        public LabInventory()
        {
            Slots = new Dictionary<int, ItemObj>(Size);
        }
    }
}