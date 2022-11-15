using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using _Scripts.SaveSystem;
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
    
    public enum MoonPhase
    {
        
    }
    
    public class DaysController : MonoSingleton<DaysController> , DataObject
    {
        public Time time = new Time(7,0);
        public int startHourPatient { get; set; } = 8;
        public int finisHourPatients { get; set; } = 18;

        public int currentDay = 1;
        public static Action DayChangeAction;
        public static Action NightStartAction;
        [SerializeField]
        private List<Time> _patientsTime = new List<Time>();
        [SerializeField]
        private Light mainLight;
        [SerializeField]
        private Color dayColor;
        [SerializeField]
        private Color nightColor;

        private bool changed;
        private Color currentColor;
        private DataObject _dataObjectImplementation;

        private void RestartDay()
        {
            time = new Time(7, 0);
            ChangeDayCall();
            //_Scripts.Store.StoreController.Instance.UpdateItem();
        }

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
            Debug.Log("daty");
            DayChangeAction?.Invoke();
        }

        private void ChangeLightColor(Color color)
        {
            mainLight.color = Color.Lerp(mainLight.color, color, UnityEngine.Time.deltaTime/2);
        }
        private void ChangeLightColorNight()
        {
            Debug.Log("Noite");
            currentColor = nightColor;
            changed = true;
        }
        private void ChangeLightColorDay()
        {
            Debug.Log("Day");
            currentColor = dayColor;
            changed = true;
        }
        private void Update()
        {
            if (!changed) return;

            //ChangeLightColor(currentColor);
            //if(mainLight.color == currentColor)
                //changed = false;
        }
        private void OnEnable()
        {
            PatientsEvents.StartDay += GeneratePatientsTimeList;
            DayChangeAction += ChangeLightColorDay;
            DayChangeAction += AddDay;
            NightStartAction += ChangeLightColorNight;
            GameManager.Sleep += RestartDay;
            GameManager.Sleep += BalanceControl.ChangeDay;
        }
        

        private void AddDay()
        {
            currentDay++;
        }

        private void OnDisable()
        {
            PatientsEvents.StartDay -= GeneratePatientsTimeList;
            DayChangeAction -= ChangeLightColorDay;
            DayChangeAction -= AddDay;
            NightStartAction -= ChangeLightColorNight;
            GameManager.Sleep -= RestartDay;
            GameManager.Sleep -= BalanceControl.ChangeDay;
        }

        private void CountTime()
        {
            
            if (time.Hours == 3)
            {
                //alerta para dormir
            }
            else
            {
                time.Minutes++;
                if (time.Minutes == 60)
                {
                    time.Minutes = 0;
                    time.Hours++;
                }

                if (time.Hours == 6)
                {
                   // ChangeDayCall();
                }

                if (time.Hours == 24)
                {
                    time.Hours = 0;
                }
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

            //HUD_Controller.Instance.UpdateTimeText();
        }

        private void Awake()
        {
            Savesystem.ClearSave();
            base.Awake();
            
            Load();
        }
        private void Start()
        {
           
            InvokeRepeating("CountTime", 0.5f, 0.5f);
            ChangeDayCall();
        }

        public void Load()
        {
            SaveDay d = (SaveDay)Savesystem.Load(this);
            // if (IsNewGame)
            // {
            //     //Developer.ClearSaves();
            //     // clear save
            //     Debug.Log("NewGame");
            //     return;
            // }
            if (d != null)
            {
                // /*variavell*/ = /*variavel*/ = data./*variavel*/;

                currentDay = d.Day;

            }
        }

        public void Save()
        {
            SaveData data = new SaveDay(currentDay);
            Savesystem.Save(data, this);
        }
    }
}