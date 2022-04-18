using System.Collections.Generic;
using _Scripts.Singleton;
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
    }
}