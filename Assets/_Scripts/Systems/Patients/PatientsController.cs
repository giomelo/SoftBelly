using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using _Scripts.U_Variables;
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
        public MedicalSymptoms Item;
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
        public Transform[] patientEnd;
        
        public Patient currentPatient;
        public NpcBase currentPatientNPC;
        
        private int amountOfPatients = 0;

        private int amountOfPatientsDay = 10;
        public void GenerateRandomOrder(ref OrderObj order)
        {
            var type = RandomEnumValues.RandomEnumValue<PotionType>();
            order.PotionType = type;
            var index = Random.Range(0, PossiblesOrders.Count - 1);
            order.Order = PossiblesOrders[index].Item;
            order.OrderDescription =
                PossiblesOrders[index]
                    .PossibleDescriptions[Random.Range(0, PossiblesOrders[index].PossibleDescriptions.Count)] +
                "Tipo: " + order.PotionType ;
            order.Money = GenerateMoney(order.PotionType);
            PatientsEvents.CurrentOrder = order;
        }


        private int GenerateMoney(PotionType t)
        {
            switch (t)
            {
                case PotionType.Chá:
                    return 10;
                case PotionType.Cataplasma:
                    return 20;
                case PotionType.Infusao:
                    return 20;
                case PotionType.Maceração:
                    return 30;
                case PotionType.Vinho:
                    return 40;
                case PotionType.Xarope:
                    return 40;
                case PotionType.Wrong:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(t), t, null);
            }
            return 0;
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
            currentPatient = patientScript;
            currentPatientNPC = npcScript;
            patientScript.SetOrder();
            patientScript.SetTime();
            StartCoroutine(patientScript.CheckTime());
            
        }

        //Check if the agent arrived the destination
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator Arrived(Patient p, NpcBase npc)
        {
            yield return new WaitForSeconds(1.0f);
            Debug.Log("Check");
            if (npc.CheckIfIsInDestination())
            {
                if (p.State == PatientState.Entering)
                {
                    p.SetState(PatientState.Waiting);
                    yield break;
                }

                p.Destroy();
                Invoke(nameof(GeneratePatient), Random.Range(0.5f, 4));
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
            PatientsEvents.StartDay += PatientsCall;
        }
        private void OnDisable()
        {
            PatientsEvents.OnOrderView -= TypeWriteText;
            PatientsEvents.OnOrderDisable -= DisableText;
            PatientsEvents.OnPatientArrived -= InitializePatient;
            PatientsEvents.StartDay -= PatientsCall;
        }

        private void Start()
        {
            // if (PatientsEvents.HasPatient)
            // {
            //     SetPatient();
            // }
            // else
            // {
            //     GeneratePatient();
            // }

           
        }

        private void PatientsCall()
        {
            
            for (int i = 0; i < amountOfPatientsDay; i++)
            {
                // escolher uma hora do dia aleatoria para entrar
                GeneratePatient();
            }
        }
        
        

        public void Initialize()
        {
            if (PatientsEvents.HasPatient)
            {
                //SetPatient();
            }
            else
            {
                GeneratePatient();
            }
        }
        private void SetPatient()
        {
            var patient = Instantiate(patientPrefab, patientEnd[Random.Range(0,patientEnd.Length)].position, Quaternion.identity).transform;
            currentPatient = patient.GetComponent<Patient>();
            currentPatient.SetState(PatientState.Waiting);
            currentPatientNPC = patient.GetComponent<NpcBase>();
            currentPatient.Order = PatientsEvents.CurrentOrder;
        }

        public void RecusePatient()
        {
            PatientsEvents.HasPatient = false;
            PatientsEvents.OnOrderDisableCall();
            currentPatient.SetState(PatientState.Leaving);
            UniversalVariables.Instance.ModifyReputation(10, false);
            StartCoroutine(Arrived(currentPatient, currentPatientNPC));
        }

        public void DeliverOrder()
        {
            PatientsEvents.OnOrderDeliveredCall(currentPatient);
        }
    }
}