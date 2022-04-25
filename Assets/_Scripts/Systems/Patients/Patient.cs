using UnityEngine;
using System;
using _Scripts.Entities.Npcs;
using _Scripts.Systems.Inventories;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;

        private void DisplayOrder()
        {
            Order.Object.SetActive(true);
        }

        public void SetOrder()
        {
            PatientsController.Instance.GenerateRandomOrder(ref Order.Order, ref Order.OrderDescription);
        }

        public void OnTriggerEnter(Collider other)
        {
            DisplayOrder();
            PatientsEvents.OnOrderViewCall(this);
        }

        private void OnTriggerExit(Collider other)
        {
            PatientsEvents.OnOrderDisableCall();
        }

        private void OnEnable()
        {
            PatientsEvents.OnOrderDelivered += Deliver;
        }

        private void OnDisable()
        {
            PatientsEvents.OnOrderDelivered -= Deliver;
        }
        

        public static void Deliver(Patient p)
        {
            if (LabInventoryHolder.Instance.Storage.CheckIfContainsKey(p.Order.Order))
            {
                LabInventoryHolder.Instance.Storage.RemoveItem(p.Order.Order);
                LabInventoryHolder.Instance.UpdateExposedInventory();
               
            }
            else
            {
               
            }  
        }
    }
}