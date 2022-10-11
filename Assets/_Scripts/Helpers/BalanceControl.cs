using _Scripts.Singleton;
using _Scripts.Systems.Patients;
using UnityEngine;

namespace _Scripts.Helpers
{
    public static class BalanceControl
    {
        public static float EasyPotion = 1;
        public static float MediumPotion = 0;
        public static float HardPotion = 0;
        // medium curve grow
        // y = a^x a = 1.1
        // hard curve grow
        // y = logb(x + 2a) a = -5.7 b = 1.1
        public static void ChangeDay()
        { 
            EasyPotion = CurveGrowth(); 
            MediumPotion = CurveGrowth();
            HardPotion = CurveGrowth();
        }

        public static float CurveGrowth()
        {
            return 0;
        }
        
        public static DificultyOfPotion? GenerateDificultyOfPotion()
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