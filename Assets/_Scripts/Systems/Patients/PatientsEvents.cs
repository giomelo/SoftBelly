using System;

namespace _Scripts.Systems.Patients
{
    public static class PatientsEvents
    {
        public static Action<Patient> OnOrderDelivered;

        public static void OnActionDeliveredCall(Patient p)
        {
            OnOrderDelivered?.Invoke(p);
        }
    }
}