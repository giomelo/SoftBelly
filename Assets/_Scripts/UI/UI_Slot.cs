using System;
using _Scripts.Systems.Item;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    /// <summary>
    /// Ui slot base components
    /// </summary>
    [Serializable]
    public struct UISlot
    {
        public Image itemImage;
        public Text amount;
        public ItemBehaviour item;
        public int slotId;
    }
    
}