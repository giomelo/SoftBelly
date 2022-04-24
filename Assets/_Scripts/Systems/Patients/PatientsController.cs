using System;
using System.Collections.Generic;
using _Scripts.Entities.Npcs;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts.Systems.Patients
{
    public class PatientsController : MonoSingleton<PatientsController>
    {
        public List<ItemBehaviour> PossiblesOrders = new List<ItemBehaviour>();
        [SerializeField]
        public TextMeshProUGUI orderText;
        [SerializeField]
        private GameObject orderTextGameObject;
        private readonly float _timePerCharacter = 0.1f;
        
        [SerializeField]
        private GameObject patientPrefab;
        private bool hasPatient = false;
        [SerializeField]
        private Transform patientStart;
        [SerializeField]
        private Transform patientEnd;
        public ItemBehaviour GenerateRandomOrder()
        {
            return PossiblesOrders[Random.Range(0, PossiblesOrders.Count - 1)];
        }

        public void GenaratePatient()
        {
            Transform patient = Instantiate(patientPrefab, patientStart.position, Quaternion.identity).transform;
            PatientsEvents.OnPatientArrivedCall(patient);
        }

        private void InitializePatient(Transform p)
        {
            p.transform.GetComponent<Patient>().SetOrder();
            p.GetComponent<NpcBase>().MoveToPosition(patientEnd.position);
        }

        private void TypeWriteText(Patient p)
        {
            orderTextGameObject.SetActive(true);
            orderText.text = "";
            UIAssistant.Instance.textWriterSingle = WriterText.Instance.AddWriter(orderText, p.Order.OrderDescription, _timePerCharacter,true, true);
        }
        private void DisableText()
        {
            orderTextGameObject.SetActive(false);
            WriterText.Instance.ResetWriter();
        }

        private void OnEnable()
        {
            PatientsEvents.OnOrderView += TypeWriteText;
            PatientsEvents.OnOrderDisable += DisableText;
            PatientsEvents.OnPatientArrived += InitializePatient;
        }
        private void OnDisable()
        {
            PatientsEvents.OnOrderView -= TypeWriteText;
            PatientsEvents.OnOrderDisable -= DisableText;
            PatientsEvents.OnPatientArrived -= InitializePatient;
        }

        private void Start()
        {
            GenaratePatient();
        }
    }
}