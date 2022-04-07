using UnityEngine;
using System;
using _Scripts.Systems.Npcs;
using _Scripts.Systems.Inventories;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        [SerializeField]
        private OrderObj _order;

        //public
        private void Start()
        {
            SetOrder();
            DisplayOrder();
        }

        private void DisplayOrder()
        {
            _order.Object.SetActive(true);
        }

        public void SetOrder()
        {
            _order.Order = PatientsController.Instance.GenerateRandomOrder();
        }

        public void OnTriggerEnter(Collider other)
        {     
                if (LabInventoryHolder.Instance.Storage.CheckIfContainsKey(_order.Order))
                {
                    LabInventoryHolder.Instance.Storage.RemoveItem(_order.Order);
                    LabInventoryHolder.Instance.UpdateExposedInventory();
                    Debug.Log("deu");
                }
                else
                {
                    Debug.Log("naodeu");
                }  
        }
    }
}