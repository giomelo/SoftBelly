﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Plants.Bases;
using _Scripts.U_Variables;
using Random = UnityEngine.Random;
using Time = _Scripts.U_Variables.Time;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        public OrderObj Order;
        
        public Time TimeToEnter;
        public PatientState State { get; private set; }

        public void SetOrder()
        {
            PatientsController.Instance.GenerateRandomOrder(ref Order);
        }

        public IEnumerator CheckTime()
        {
            if (DaysController.Instance.time.Hours == TimeToEnter.Hours &&
                DaysController.Instance.time.Minutes == TimeToEnter.Minutes)
            {
                SetState(PatientState.Entering);
                StartCoroutine(Arrived());
                yield break;
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(CheckTime());
        }
        public void SetTime()
        {
            TimeToEnter = DaysController.Instance.GenerateRandomTime();
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

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
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
                    MoveToPosition(PatientsController.Instance.patientEnd[Random.Range(0, PatientsController.Instance.patientEnd.Length -1 )].position);
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
    }
}