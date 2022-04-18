using System;
using System.Collections.Generic;
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

        public ItemBehaviour GenerateRandomOrder()
        {
            return PossiblesOrders[Random.Range(0, PossiblesOrders.Count - 1)];
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
        }
        private void OnDisable()
        {
            PatientsEvents.OnOrderView -= TypeWriteText;
            PatientsEvents.OnOrderDisable -= DisableText;
        }
    }
}