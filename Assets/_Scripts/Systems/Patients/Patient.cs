using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using Random = UnityEngine.Random;

namespace _Scripts.Systems.Patients
{
    [Serializable]
    internal struct ClothAcessories
    {
        public List<MeshRenderer> hats;
        public List<MeshRenderer> topCloth;
        public List<MeshRenderer> bottomCloth;
        public List<MeshRenderer> acessorie;
    }
    
    public class Patient : NpcBase
    {
        public OrderObj Order;

        public SocialLabel label;
        public PatientState State { get; private set; }
        
        public List<Material> colors;
        [SerializeField]
        private ClothAcessories poor;
        [SerializeField]
        private ClothAcessories noble;

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
            SetState(PatientState.Entering);
            StartCoroutine(Arrived());
        }

        private void SetCloth()
        {
            switch (label)
            {
                case SocialLabel.NOBRE:
                    SetItem(noble);
                    break;
                case SocialLabel.POBRE:
                    SetItem(poor);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetItem(ClothAcessories list)
        {
            MeshRenderer aux = list.hats[Random.Range(0, list.hats.Count)];
            aux.gameObject.SetActive(true);
            aux.material = colors[Random.Range(0,colors.Count)];
            aux = list.topCloth[Random.Range(0, list.topCloth.Count)];
            aux.gameObject.SetActive(true);
            aux.material = colors[Random.Range(0,colors.Count)];
            aux = list.bottomCloth[Random.Range(0, list.bottomCloth.Count)];
            aux.gameObject.SetActive(true);
            aux.material = colors[Random.Range(0,colors.Count)];
            aux = list.acessorie[Random.Range(0, list.acessorie.Count)];
            aux.gameObject.SetActive(true);
            aux.material = colors[Random.Range(0,colors.Count)];
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