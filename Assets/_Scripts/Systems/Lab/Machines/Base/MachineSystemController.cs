using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.U_Variables;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.Base
{
    public class MachineSystemController : MonoSingleton<MachineSystemController>
    {
        [SerializeField]
        public List<MachineHolder> allMachines = new List<MachineHolder>();
        private void Start()
        {
            LabTimeController.Instance.DisplayMachines();
        }

        public void UnlockMachine()
        {
            switch (UniversalVariables.Instance.Nivel)
            {
                case 2:
                    allMachines[2].CurrentMachine.UnlockMachine();                    break;
                case 3:
                    allMachines[3].CurrentMachine.UnlockMachine(); 
                    break;
                case 4:
                    break;
                case 5:
                    // tecnicamente fim de jogo
                    break;
            }
        }
        
        private void OnEnable()
        {
            GameManager.PromotionLevel += UnlockMachine;
        }
        private void OnDisable()
        {
            GameManager.PromotionLevel -= UnlockMachine;
        }
    }
}