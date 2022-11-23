using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using Random = UnityEngine.Random;

namespace _Scripts.Systems.Patients
{
   
   
    public class Patient : NpcBase
    {
        public OrderObj Order;

        public SocialLabel label;
        public PatientState State { get; private set; }
        

        [SerializeField]
        private Renderer meshCouro;
        [SerializeField]
        private Renderer meshcouro2;
        [SerializeField]
        private Renderer meshpele;
        [SerializeField]
        private Renderer Meshcabelo;
        [SerializeField]
        private Renderer meshRoupa;

        private Animator anim;

        public void SetOrder()
        {
            SetLabel();
            //SetCloth();
            PatientsController.Instance.GenerateRandomOrder(ref Order);
        }
        public void SetLabel()
        {
            var type = RandomEnumValues.RandomEnumValue<SocialLabel>();
            label = type;
        }

        private void Start()
        {
            anim = transform.GetComponent<Animator>();
            SetState(PatientState.Entering);
            StartCoroutine(Arrived());
            SetCloth();
        }

        private void SetCloth()
        {
            switch (label)
            {
                case SocialLabel.NOBRE:
                    
                    SetItem();
                    break;
                case SocialLabel.POBRE:
            
                    SetItem();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetItem()
        {
            Color c = PatientsController.Instance.listaRoupaPobre[Random.Range(0, PatientsController.Instance.listaRoupaPobre.Count)];
            meshRoupa.material.color = c;
            c = PatientsController.Instance.coresPeles[Random.Range(0, PatientsController.Instance.coresPeles.Count)];
            meshpele.material.color = c;
            c = PatientsController.Instance.coreCourouPobre[Random.Range(0, PatientsController.Instance.coreCourouPobre.Count)];
            meshcouro2.material.color = c;
            c = PatientsController.Instance.coreCourouPobre[Random.Range(0, PatientsController.Instance.coreCourouPobre.Count)];
            meshCouro.material.color = c;
            c = Random.ColorHSV();
            Meshcabelo.material.color = c;
        }

        public void Destroy()
        {
            Destroy(gameObject);
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
           // if(State != PatientState.Waiting) return;
            PatientsEvents.OnOrderDisableCall();
        }
        
        public IEnumerator Arrived()
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Check");
            if (CheckIfIsInDestination())
            {
                if (State == PatientState.Entering)
                {
                    SetState(PatientState.Waiting);
                    yield break;
                }
                StopAllCoroutines();
                Destroy();
                
                yield break;

            }
            StartCoroutine(Arrived());
        }
        
        /// <summary>
        /// Chek the patients state to do something
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>

        private void CheckState()
        {
            switch (State)
            {
                case PatientState.Entering:
                    // set animation walk
                    
                    anim.SetTrigger("walk");
                    MoveToPosition(PatientsController.Instance.patientEnd[PatientsController.Instance.fila.Count -1].position);
                    break;
                case PatientState.Waiting:
                    // set animation idle
                    anim.SetTrigger("idle");
                    break;
                case PatientState.Leaving:
                    StartCoroutine(Arrived());
                    // set animation walk
                    anim.SetTrigger("walk");
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