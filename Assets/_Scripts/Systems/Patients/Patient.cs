﻿using UnityEngine;
using System;
using _Scripts.Systems.Npcs;
using _Scripts.Systems.Inventories;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;

        //public
        private void Start()
        {
            SetOrder();
        }

        private void DisplayOrder()
        {
            Order.Object.SetActive(true);
        }

        public void SetOrder()
        {
            Order.Order = PatientsController.Instance.GenerateRandomOrder();
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
        

        public void Deliver(Patient p)
        {
            if (LabInventoryHolder.Instance.Storage.CheckIfContainsKey(p.Order.Order))
            {
                LabInventoryHolder.Instance.Storage.RemoveItem(p.Order.Order);
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