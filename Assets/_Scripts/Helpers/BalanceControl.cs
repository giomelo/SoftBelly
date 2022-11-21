using System;
using _Scripts.Singleton;
using _Scripts.Systems.Patients;
using _Scripts.U_Variables;
using Random = UnityEngine.Random;

namespace _Scripts.Helpers
{
    public static class BalanceControl
    {
        public static float EasyPotion = 1;
        public static float MediumPotion = 0;
        public static float HardPotion = 0;

        private static float a = 1.2f;
        private static float b = 1.1f;
        private static float c = -8f;

        public static float p = -1.9f;
        public static float r = 50;
        // medium curve grow
        // y = a^x a = 1.1
        // hard curve grow
        // y = logb(x + 2a) a = -5.7 b = 1.1
        
        // easy curve grow
        // y = px + r
        
        // amount of patients curve
        public static int maxPatientsPerDay = 7;
        
        public static float d = 0.4f;
        public static float e = 3f;
        public static void ChangeDay()
        { 
            EasyPotion = CurveGrowth(DificultyOfPotion.Easy); 
            MediumPotion = CurveGrowth(DificultyOfPotion.Medium);
            HardPotion = CurveGrowth(DificultyOfPotion.Complex);
            GenerateAmountOfPatients();
        } 

        public static float CurveGrowth(DificultyOfPotion dif)
        {
            float returnValue = dif switch
            {
                DificultyOfPotion.Easy => p * DaysController.Instance.currentDay + r,
                DificultyOfPotion.Medium => (float) Math.Pow(a, DaysController.Instance.currentDay),
                DificultyOfPotion.Complex => (float) Math.Log(DaysController.Instance.currentDay + 2 * c, b),
                _ => throw new ArgumentOutOfRangeException(nameof(dif), dif, null)
            };

            return returnValue;
        }

        public static void GenerateAmountOfPatients()
        {
            PatientsController.Instance.amountOfPatientsDay = (int) (d * DaysController.Instance.currentDay + e) <= 7 ? (int) (d * DaysController.Instance.currentDay + e) : 7;
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