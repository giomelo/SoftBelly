using System.Collections.Generic;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class MachineSystemController : MonoSingleton<MachineSystemController>
    {
        public static int MachinesId = 0;
        [SerializeField]
        public List<MachineHolder> allMachines = new List<MachineHolder>();
        private void Start()
        {
            MachinesId = 0;
            foreach (var machine in allMachines)
            {
                machine.CurrentMachine.MachineId = MachinesId;
                MachinesId++;
            }

            LabTimeController.Instance.DisplayMachines();
        }
    }
}