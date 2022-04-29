using System;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Systems.Lab
{
    public enum MachineSlotType
    {
        Ingredient,
        Result
    }
    [Serializable]
    public struct UIMachineSlot
    {
        public ItemObj MachineSlot;
        public Image Image;
        public Image HighImage;
        public Text Amount;
        public MachineSlotType Type;
        public int slotId;//each slot in the machina has a diferent slotId
        public int maxPerSlot;
        [EnumFlags]
        public ItemType itemRequired;
    }
}