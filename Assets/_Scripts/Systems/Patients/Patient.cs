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
                if (LabInventory.Instance.CheckIfContainsKey(_order.Order))
                {
                    LabInventory.Instance.RemoveItem(_order.Order);
                    Debug.Log("deu");
                }
            else
            {
                Debug.Log("naodeu");
            }  
        }
    }
}