using _Scripts.Systems.Patients;
using UnityEngine;

namespace _Scripts.Singleton
{
    /// <summary>
    /// Dont destroy controller
    /// </summary>
    public class GeneralGameController : MonoSingleton<GeneralGameController>
    {
        public bool HasPatient { get; set; }
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

        private void NoPatient(Patient p)
        {
            HasPatient = false;
        }
    }
}