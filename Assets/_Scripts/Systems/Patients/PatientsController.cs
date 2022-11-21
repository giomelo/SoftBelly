using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Entities.Npcs;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Plants.Bases;
using _Scripts.U_Variables;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts.Systems.Patients
{
    [Serializable]
    public struct Description
    {
        [TextArea]
        public string Descriptionstring;
        public bool isWrong;
    }
    
    [Serializable]
    public struct OrdersAndDescriptions
    {
        public MedicalSymptoms Item;
        public List<Description> PossibleDescriptions;
    }
    [Serializable]
    public struct PotionTypeAndNivel
    {
        public PotionType PotionType;
        public DificultyOfPotion Dificulty;

        public PotionTypeAndNivel(PotionType potionType, DificultyOfPotion dificulty)
        {
            PotionType = potionType;
            Dificulty = dificulty;
        }
    }

    public enum DificultyOfPotion
    {
        Easy,
        Medium,
        Complex
    }

    public class PatientsController : MonoSingleton<PatientsController>
    {
        public List<OrdersAndDescriptions> PossiblesOrders = new List<OrdersAndDescriptions>();
        [SerializeField]
        public TextMeshProUGUI orderText;
        [SerializeField]
        private GameObject orderTextGameObject;

        private const float TimePerCharacter = 0.05f;

        [SerializeField]
        private GameObject patientPrefab;
        [SerializeField]
        public Transform patientStart;
        [SerializeField]
        public Transform exit;
        public Transform[] patientEnd;
        
        public Patient currentPatient;

        private int amountOfPatients = 0;
        private PotionBase _playerPotion = null;
        [SerializeField]
        private List<PotionTypeAndNivel> potionsDificulty = new List<PotionTypeAndNivel>();
        public int amountOfPatientsDay = 7;
        public List<Color> coreCourouPobre;
        public List<Color> coresPeles;
        public List<Color> listaRoupaPobre;
        public List<Color> roupaRico;

        public List<Patient> fila = new List<Patient>();
        public bool HasPatient { get; set; }
       
        public void GenerateRandomOrder(ref OrderObj order)
        {
            var options = potionsDificulty.Where(p => p.Dificulty == BalanceControl.GenerateDificultyOfPotion()).ToList();
            var type = options[Random.Range(0, options.Count)].PotionType;
            order.PotionType = type;
            var index = Random.Range(0, PossiblesOrders.Count - 1);
            order.Order = PossiblesOrders[index].Item;
            order.OrderDescription =
                PossiblesOrders[index]
                    .PossibleDescriptions[Random.Range(0, PossiblesOrders[index].PossibleDescriptions.Count)].Descriptionstring +
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
        public void GeneratePatient()
        {
            var patient = Instantiate(patientPrefab, patientStart.position, Quaternion.identity).transform;
            PatientsEvents.OnPatientArrivedCall(patient);
        }
        
        //Set patient order and move to destination
        private void InitializePatient(Transform p)
        {
            if (!p.TryGetComponent<Patient>(out var patientScript)) return;
            patientScript.SetOrder();
            if (fila.Contains(patientScript)) return;
            fila.Add(patientScript);
            //patientScript.SetTime();
            //StartCoroutine(patientScript.CheckTime());
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
            //PatientsEvents.StartDay += PatientsCall;
            PatientsEvents.OnOrderDelivered += Deliver;
        }
        private void OnDisable()
        {
            PatientsEvents.OnOrderView -= TypeWriteText;
            PatientsEvents.OnOrderDisable -= DisableText;
            PatientsEvents.OnPatientArrived -= InitializePatient;
            //PatientsEvents.StartDay -= PatientsCall;
            PatientsEvents.OnOrderDelivered -= Deliver;
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
               // GeneratePatient();
            }
        }
        private void SetPatient()
        {
            var patient = Instantiate(patientPrefab, patientEnd[Random.Range(0,patientEnd.Length)].position, Quaternion.identity).transform;
            currentPatient = patient.GetComponent<Patient>();
            currentPatient.SetState(PatientState.Waiting);
            currentPatient.Order = PatientsEvents.CurrentOrder;
        }

        public void RecusePatient()
        {
            PatientsEvents.HasPatient = false;
            PatientsEvents.OnOrderDisableCall();
            currentPatient.SetState(PatientState.Leaving);
            fila.Remove(currentPatient);
            UpdateLine();
            
            UniversalVariables.Instance.ModifyReputation(10, false);
            StartCoroutine(currentPatient.Arrived());
        }

        private void UpdateLine()
        {
            for (int i = 0; i < fila.Count; i++)
            {
                fila[i].MoveToPosition(patientEnd[i].position);
            }
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
                currentPatient.SetState(PatientState.Leaving);
                GiveMoney();
                GiveReputation(10);
                
                switch (currentPatient.label)
                {
                    case SocialLabel.NOBRE:
                        GiveSocialStatus(10, true);
                        break;
                    case SocialLabel.POBRE:
                        GiveSocialStatus(10, false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                PatientsEvents.HasPatient = false;
                fila.Remove(currentPatient);
                UpdateLine();
                StartCoroutine(currentPatient.Arrived());
            }
            else
            {
                //nao possui o item
            }  
        }

        private void GiveReputation(int amount)
        {
            UniversalVariables.Instance.ModifyReputation(amount, true);
        }

        private void GiveSocialStatus(int amount, bool up)
        {
            UniversalVariables.Instance.ModifySocialAligment(amount, up);
        }
        private void GiveMoney()
        {
            int initialMoney = currentPatient.Order.Money;
            var aux = _playerPotion.Cure.Where(s => s.Symptoms == currentPatient.Order.Order);
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


        public void DeliverOrder()
        {
            PatientsEvents.OnOrderDeliveredCall(currentPatient);
        }
    }
}