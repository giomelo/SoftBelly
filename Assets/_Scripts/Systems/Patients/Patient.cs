using UnityEngine;
using System;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;
        public PatientState State { get; private set; }

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
        //Called when the player clicks a patient
        private void Deliver(Patient p)
        {
            if (LabInventoryHolder.Instance.Storage.CheckIfContainsKey(p.Order.Order))
            {
                LabInventoryHolder.Instance.Storage.RemoveItem(p.Order.Order);
                LabInventoryHolder.Instance.UpdateExposedInventory();
                SetState(PatientState.Leaving);
                
            }
            else
            {
               //nao possui o item
            }  
        }

        private void CheckState()
        {
            switch (State)
            {
                case PatientState.Entering:
                    MoveToPosition(PatientsController.Instance.patientEnd.position);
                    break;
                case PatientState.Waiting:
                    break;
                case PatientState.Leaving:
                    MoveToPosition(PatientsController.Instance.patientStart.position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetState(PatientState state)
        {
            State = state;
            CheckState();
        }
    }
}