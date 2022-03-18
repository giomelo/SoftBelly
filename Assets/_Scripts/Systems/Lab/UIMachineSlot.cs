using System;
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
    public struct MachineSlot
    {
        public ItemBehaviour item;
        public int amount;
     
    }
    [Serializable]
    public struct UIMachineSlot
    {
        public MachineSlot MachineSlot;
        public Image Image;
        public Text Amount;
        public GameObject Slot;
        public MachineSlotType Type;
        public int slotId;//each slot in the machina has a diferent slotId
    }
}