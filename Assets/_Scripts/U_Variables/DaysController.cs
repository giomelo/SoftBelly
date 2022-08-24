using System;
using _Scripts.Singleton;
using _Scripts.UI;

namespace _Scripts.U_Variables
{
    public class DaysController : MonoSingleton<DaysController>
    {
        public float Hours { get; set; } = 7;
        public float Seconds { get; set; }

        public Action DayChangeAction;
        public Action NightStartAction;
 
        private void CountTime()
        {
            Seconds++;
            
            if (Seconds == 60)
            {
                Seconds = 0;
                Hours++;
            }

            if (Hours == 24)
            {
                Hours = 0;
            }

            HUD_Controller.Instance.UpdateTimeText();
        }

        private void Start()
        {
            InvokeRepeating("CountTime", 1, 1);
        }
    }
}