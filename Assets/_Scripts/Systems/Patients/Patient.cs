using UnityEngine;
using System;
using System.Collections;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;

        public SocialLabel label;
        public PatientState State { get; private set; }

        public void SetOrder()
        {
            PatientsController.Instance.GenerateRandomOrder(ref Order);
        }
        public void SetLabel()
        {
            var type = RandomEnumValues.RandomEnumValue<SocialLabel>();
            label = type;
        }

        private void Start()
        {
            SetState(PatientState.Entering);
            StartCoroutine(Arrived());
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (State != PatientState.Waiting) return;
            PatientsEvents.CurrentOrder = this.Order;
            PatientsController.Instance.currentPatient = this;
            PatientsEvents.OnOrderViewCall(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if(State != PatientState.Waiting) return;
            PatientsEvents.OnOrderDisableCall();
        }
        
        public IEnumerator Arrived()
        {
            yield return new WaitForSeconds(1.0f);
            Debug.Log("Check");
            if (CheckIfIsInDestination())
            {
                if (State == PatientState.Entering)
                {
                    SetState(PatientState.Waiting);
                    yield break;
                }

                Destroy();
                yield break;

            }
            StartCoroutine(Arrived());
        }

        private void CheckState()
        {
            switch (State)
            {
                case PatientState.Entering:
                    MoveToPosition(PatientsController.Instance.patientEnd[PatientsController.Instance.fila.Count - 1].position);
                    break;
                case PatientState.Waiting:
                    break;
                case PatientState.Leaving:
                    MoveToPosition(PatientsController.Instance.exit.position);
                    
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

        private void OnEnable()
        {
            PatientsEvents.StartNight += Out;
        }

        private void OnDisable()
        {
            PatientsEvents.StartNight -= Out;
        }
        private void Out()
        {
            SetState(PatientState.Leaving);
        }
    }
}