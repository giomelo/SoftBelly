using System;
using UnityEngine;

namespace _Scripts.Systems.Patients
{
    public static class PatientsEvents
    {
        public static Action<Patient> OnOrderDelivered;
        public static Action<Patient> OnOrderView;
        public static Action OnOrderDisable;
        public static Action<Transform> OnPatientArrived;

        public static bool HasPatient;
        public static OrderObj CurrentOrder;
        public static void OnOrderDisableCall()
        {
            OnOrderDisable?.Invoke();
        }

        public static void OnOrderDeliveredCall(Patient p)
        {
            OnOrderDelivered?.Invoke(p);
        }

        public static void OnOrderViewCall(Patient p)
        {
            OnOrderView?.Invoke(p);
        }
        public static void OnPatientArrivedCall(Transform p)
        {
            HasPatient = true;
            OnPatientArrived?.Invoke(p);
        }
    }
}