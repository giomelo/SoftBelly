using System;
using _Scripts.Singleton;
using _Scripts.Systems.Patients;
using _Scripts.UI;
using Random = UnityEngine.Random;

namespace _Scripts.U_Variables
{
    [Serializable]
    public struct Time
    {
        public int Hours;
        public int Minutes;

        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }
    }
    
    public class DaysController : MonoSingleton<DaysController>
    {

        public Time time = new Time(7,0);
        public int startHourPatient { get; set; } = 8;
        public int finisHourPatients { get; set; } = 18;
        

        public Action DayChangeAction;
        public Action NightStartAction;

        public Time GenerateRandomTime()
        {
            return new Time(Random.Range(8, 18), Random.Range(0, 59));
        }

        private void CountTime()
        {
            time.Minutes++;
            
            if (time.Minutes == 60)
            {
                time.Minutes = 0;
                time.Hours++;
            }

            if (time.Hours == 24)
            {
                time.Hours = 0;
            }

            if (time.Hours == startHourPatient && !PatientsEvents.Day)
            {
                PatientsEvents.OnStartDayCall();
                
            }

            HUD_Controller.Instance.UpdateTimeText();
        }

        private void Start()
        {
            InvokeRepeating("CountTime", 1, 1);
        }
    }
}