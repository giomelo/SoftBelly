using _Scripts.Singleton;
using _Scripts.Systems.Patients;
using UnityEngine;

namespace _Scripts.Helpers
{
    public class BalanceControl
    {
        public static float EasyPotion = 1;
        public static float MediumPotion = 0;
        public static float HardPotion = 0;
        // easy curve grow
        public static void ChangeDay()
        {
            //EasyPotion = curveGrowth(day);
            //MediumPotion = curveGrowth(day);
           // HardPotion = curveGrowth(day);
        }

        public DificultyOfPotion? GenerateDificultyOfPotion()
        {
            var random = Random.Range(0, 1);
            var total = EasyPotion + MediumPotion + HardPotion;
            var probabilityEasy = EasyPotion / total;
            var probabilityMedium = MediumPotion / total;
            var probabilityHard = HardPotion / total;


            if (random <= probabilityHard) return DificultyOfPotion.Complex;
            if (random <= probabilityMedium) return DificultyOfPotion.Medium;
            if (random <= probabilityEasy) return DificultyOfPotion.Easy;

            return null;
        }
    }
}