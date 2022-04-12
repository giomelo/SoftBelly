using System;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Systems.Patients
{
    public class PatientsController : MonoSingleton<PatientsController>
    {
        public List<ItemBehaviour> PossiblesOrders = new List<ItemBehaviour>();

        public ItemBehaviour GenerateRandomOrder()
        {
            return PossiblesOrders[Random.Range(0, PossiblesOrders.Count - 1)];
        }
    }
}