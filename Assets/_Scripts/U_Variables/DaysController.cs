using System;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Patients;
using _Scripts.UI;
using UnityEngine;
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
        [SerializeField]
        private List<Time> _patientsTime = new List<Time>();

        public Time GenerateRandomTime(int minH, int maxH, int minM, int maxM)
        {
            return new Time(Random.Range(minH, maxH), Random.Range(minM, maxM));
        }

        public void GeneratePatientsTimeList()
        {
            for (int i = 0; i < PatientsController.Instance.amountOfPatientsDay; i++)
            {
                Time t = GenerateRandomTime(startHourPatient, finisHourPatients, 0, 59);
                _patientsTime.Add(t);
            }
        }

        private void ChangeNightCall()
        {
            NightStartAction?.Invoke();
        }
        private void ChangeDayCall()
        {
            DayChangeAction?.Invoke();
        }


        private void OnEnable()
        {
            PatientsEvents.StartDay += GeneratePatientsTimeList;
        }

        private void OnDisable()
        {
            PatientsEvents.StartDay -= GeneratePatientsTimeList;
        }

        private void CountTime()
        {
            time.Minutes++;
            
            if (time.Minutes == 60)
            {
                time.Minutes = 0;
                time.Hours++;
            }

            if (time.Hours == 6)
            {
                ChangeDayCall();
            }

            if (time.Hours == 24)
            {
                time.Hours = 0;
                _Scripts.Store.StoreController.Instance.UpdateItem();
            }

            if (time.Hours == startHourPatient && !PatientsEvents.Day)
            {
                PatientsEvents.OnStartDayCall();
            }

            if (time.Hours == finisHourPatients && !PatientsEvents.Night)
            {
                ChangeNightCall();
                PatientsEvents.OnStartNightCall();
            }

            if (_patientsTime.Contains(time))
            {
                PatientsController.Instance.GeneratePatient();
                _patientsTime.Remove(time);
            }

            HUD_Controller.Instance.UpdateTimeText();
        }

        private void Start()
        {
            InvokeRepeating("CountTime", 1, 1);
        }
    }
}