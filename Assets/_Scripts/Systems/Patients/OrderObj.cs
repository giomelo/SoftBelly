﻿using System;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Patients
{
    [Serializable]
    public struct OrderObj
    {
        public MedicalSymptoms Order;
        public Sprite OrderSprite;
        [TextArea]
        public string OrderDescription;
        public int Money;
        public PotionType PotionType;
    }
}