using System;
using _Scripts.Systems.Item;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    [Serializable]
    public struct UISlot
    {
        public Image itemImage;
        public Text amount;
        public ItemBehaviour item;
        public GameObject obj;
    }
    
}