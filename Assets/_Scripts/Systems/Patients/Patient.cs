using UnityEngine;
using System;
using System.Linq;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Plants.Bases;
using _Scripts.U_Variables;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;
        private PotionBase _playerPotion = null;
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
            _playerPotion = LabInventoryHolder.Instance.Storage.CheckIfContainsKey(p.Order.Order, p.Order.PotionType);
            if (_playerPotion != null)
            {
                LabInventoryHolder.Instance.Storage.RemoveItem(_playerPotion);
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
            int initialMoney = Order.Money;
            var aux = _playerPotion.Cure.Where(s => s.Symptoms == Order.Order);
            SymptomsNivel s;
            foreach (var i in aux)
            {
                s = i;
            }

            s.Nivel = (SymtomsNivel) 0;
            switch (s.Nivel)
            {
                case SymtomsNivel.Medium:
                    initialMoney += 10;
                    break;
                case SymtomsNivel.Strong:
                    initialMoney += 20;
                    break;
            }
            
            UniversalVariables.Instance.ModifyMoney(initialMoney, true);
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