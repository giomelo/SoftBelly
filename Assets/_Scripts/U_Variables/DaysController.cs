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
    public struct Timee
    {
        public int Hours;
        public int Minutes;

        public Timee(int hours, int minutes)
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
        public Timee Timee = new Timee(7,50);
        public int startHourPatient { get; set; } = 8;
        public int finisHourPatients { get; set; } = 18;

        public int currentDay = -1;
        public static Action DayChangeAction;
        public static Action NightStartAction;
        [SerializeField]
        private List<Timee> _patientsTime = new List<Timee>();
        private List<Timee> _patientsTimeInitial = new List<Timee>();
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
            Timee = new Timee(7, 50);
            ChangeDayCall();
            //_Scripts.Store.StoreController.Instance.UpdateItem();
        }

        public Timee GenerateRandomTime(int minH, int maxH, int minM, int maxM)
        {
            return new Timee(Random.Range(minH, maxH), Random.Range(minM, maxM));
        }

        public void GeneratePatientsTimeList()
        {
            _patientsTime.Clear();
            Timee time = new Timee(8, 10);
            _patientsTime.Add(time);
            for (int i = 0; i < PatientsController.Instance.amountOfPatientsDay -1; i++)
            {
                Timee t = GenerateRandomTime(startHourPatient, finisHourPatients, 0, 59);
                _patientsTime.Add(t);
            }
            _patientsTimeInitial = _patientsTime;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeNightCall();
                PatientsEvents.OnStartNightCall();
            }
            if (!changed) return;

            ChangeLightColor(currentColor);
            if(mainLight.color == currentColor)
                changed = false;

              
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
            Debug.Log("AddDay");
            currentDay++;
            HUD_Controller.Instance.DiaDisplay();
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
            
            if (Timee.Hours == 3)
            {
                //alerta para dormir
                HUD_Controller.Instance.AvisoDromir();
            }
            else
            {
                if (Timee.Hours == 5)
                {
                    // ChangeDayCall();
                }
                else
                {
                    Timee.Minutes++;
                }
                if (Timee.Minutes == 60)
                {
                    Timee.Minutes = 0;
                    Timee.Hours++;
                }

                if (Timee.Hours == 6)
                {
                   // ChangeDayCall();
                }

                if (Timee.Hours == 24)
                {
                    Timee.Hours = 0;
                }
            }
            if (Timee.Hours == startHourPatient && !PatientsEvents.Day)
            {
                PatientsEvents.OnStartDayCall();
            }

            if (Timee.Hours == finisHourPatients && !PatientsEvents.Night)
            {
                ChangeNightCall();
                PatientsEvents.OnStartNightCall();
            }

            if (_patientsTime.Contains(Timee))
            {
                PatientsController.Instance.GeneratePatient();
                _patientsTime.Remove(Timee);
                if (_patientsTime.Count == 0)
                    HUD_Controller.Instance.EndDay();
            }

            //HUD_Controller.Instance.UpdateTimeText();
        }

        private void Awake()
        {
            //Savesystem.ClearSave();
            base.Awake();
           
            Load();
            BalanceControl.ChangeDay();
        }
        private void Start()
        {
            HUD_Controller.Instance.DiaDisplay();
            InvokeRepeating("CountTime", 0.5f, 0.5f);
           // ChangeDayCall();
        }

        public void Load()
        {
            SaveDay d = (SaveDay)Savesystem.Load(GetType().ToString());
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
                _patientsTime = d.patients;

            }
        }

        public void Save()
        {
            SaveData data = new SaveDay(currentDay, _patientsTimeInitial);
            Savesystem.Save(data, GetType().ToString());
        }
    }
}