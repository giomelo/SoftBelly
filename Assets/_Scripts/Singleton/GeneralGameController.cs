using _Scripts.Systems.Patients;
using UnityEngine;

namespace _Scripts.Singleton
{
    public class GeneralGameController : MonoSingleton<GeneralGameController>
    {
        public bool HasPatient {get; set; }
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
            PatientsEvents.OnPatientArrived += SetPatient;
            PatientsEvents.OnOrderDelivered += NoPatient;
        }

        private void OnDisable()
        {
            PatientsEvents.OnPatientArrived -= SetPatient;
            PatientsEvents.OnOrderDelivered -= NoPatient;
        }
        private void SetPatient(Transform obj)
        {
            HasPatient = true;
        }
        public void NoPatient(Patient p)
        {
            HasPatient = false;
        }
    }
}