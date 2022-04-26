using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
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
    [Serializable]
    public struct OrdersAndDescriptions
    {
        public ItemBehaviour Item;
        [TextArea]
        public List<string> PossibleDescriptions;
    }
    
    public class PatientsController : MonoSingleton<PatientsController>
    {
        public List<OrdersAndDescriptions> PossiblesOrders = new List<OrdersAndDescriptions>();
        [SerializeField]
        public TextMeshProUGUI orderText;
        [SerializeField]
        private GameObject orderTextGameObject;

        private const float TimePerCharacter = 0.1f;

        [SerializeField]
        private GameObject patientPrefab;
        [SerializeField]
        public Transform patientStart;
        public Transform patientEnd;
        public void GenerateRandomOrder(ref ItemBehaviour item, ref string description)
        {
            var index = Random.Range(0, PossiblesOrders.Count - 1);
            item = PossiblesOrders[index].Item;
            description = PossiblesOrders[index].PossibleDescriptions[Random.Range(0, PossiblesOrders[index].PossibleDescriptions.Count)];
        }
        
        //Instantiate patient
        private void GeneratePatient()
        {
            var patient = Instantiate(patientPrefab, patientStart.position, Quaternion.identity).transform;
            PatientsEvents.OnPatientArrivedCall(patient);
        }
        
        //Set patient order and move to destination
        private void InitializePatient(Transform p)
        {
            if (!p.TryGetComponent<NpcBase>(out var npcScript)) return;
            if (!p.TryGetComponent<Patient>(out var patientScript)) return;
            patientScript.SetOrder();
            patientScript.SetState(PatientState.Entering);
            StartCoroutine(Arrived(patientScript, npcScript));
        }
        
        //Check if the agent arrived the destination
        private IEnumerator Arrived(Patient p, NpcBase npc)
        {
            yield return new WaitForSeconds(1.0f);
            if (npc.CheckIfIsInDestination())
            {
                p.SetState(PatientState.Waiting);
                yield break;
            }
            StartCoroutine(Arrived(p, npc));
        }

        private void TypeWriteText(Patient p)
        {
            orderTextGameObject.SetActive(true);
            orderText.text = "";
            UIAssistant.Instance.textWriterSingle = WriterText.Instance.AddWriter(orderText, p.Order.OrderDescription, TimePerCharacter,true, true);
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
            GeneratePatient();
        }
    }
}