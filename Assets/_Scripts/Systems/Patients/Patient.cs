using UnityEngine;
using System;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.U_Variables;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;
        public PatientState State { get; private set; }

        public void SetOrder()
        {
            PatientsController.Instance.GenerateRandomOrder(ref Order);
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (State != PatientState.Waiting) return;
            PatientsEvents.OnOrderViewCall(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if(State != PatientState.Waiting) return;
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
            Debug.Log("Entregado");
            var item = LabInventoryHolder.Instance.Storage.CheckIfContainsKey(p.Order.Order, p.Order.PotionType);
            if (item != null)
            {
                LabInventoryHolder.Instance.Storage.RemoveItem(item);
                LabInventoryHolder.Instance.UpdateExposedInventory();
                SetState(PatientState.Leaving);
                GiveMoney();
                PatientsEvents.HasPatient = false;
                StartCoroutine(PatientsController.Instance.Arrived(PatientsController.Instance.currentPatient, PatientsController.Instance.currentPatientNPC));
            }
            else
            {
               //nao possui o item
            }  
        }

        private void GiveMoney()
        {
            UniversalVariables.Instance.ModifyMoney(Order.Money, true);
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

        // ReSharper disable Unity.PerformanceAnalysis
        public void SetState(PatientState state)
        {
            State = state;
            CheckState();
        }
    }
}