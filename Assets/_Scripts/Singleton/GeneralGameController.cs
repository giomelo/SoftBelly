using _Scripts.Systems.Patients;
using UnityEngine;

namespace _Scripts.Singleton
{
    /// <summary>
    /// Dont destroy controller
    /// </summary>
    public class GeneralGameController : MonoSingleton<GeneralGameController>
    {
     
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
           // PatientsEvents.OnPatientArrived += SetPatient;
        }

        private void OnDisable()
        {
           //PatientsEvents.OnPatientArrived -= SetPatient;
        }
    }
}