using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plants.Bases;
using _Scripts.U_Variables;
using Systems.Plantation;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Systems.Lab
{
    public class DailyPlantDrop : MonoSingleton<DailyPlantDrop>
    {
        [SerializeField]
        private List<ItemBehaviour> dropType = new List<ItemBehaviour>();

        public ItemBehaviour CurrentDrop;

        public void GenerateDrop()
        {
            CurrentDrop = dropType[Random.Range(0, dropType.Count)];
        }

        private void OnEnable()
        {
            DaysController.DayChangeAction += GenerateDrop;
        }
        private void OnDisable()
        {
            DaysController.DayChangeAction -= GenerateDrop;
        }

        public void Collect()
        {
            if (CurrentDrop == null) return;

            if(GameManager.Instance.plantStorage.Storage.itensType.HasFlag(CurrentDrop.ItemType))
                GameManager.Instance.plantStorage.Storage.AddItem(1, CurrentDrop);
            else  if(GameManager.Instance.labStorage.Storage.itensType.HasFlag(CurrentDrop.ItemType))
                GameManager.Instance.plantStorage.Storage.AddItem(1, CurrentDrop);
            CurrentDrop = null;
        }
        
    }
}