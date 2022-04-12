using System;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Patients
{
    [Serializable]
    public struct OrderObj
    {
        public ItemBehaviour Order;
        public Sprite OrderSprite;
        public GameObject Object;
    }
}