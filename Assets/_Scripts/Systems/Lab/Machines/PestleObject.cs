using System;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class PestleObject : DragObject
    {
        [SerializeField]
        private Pestle Machine;
        public void StartDrag()
        {
            canDrag = true;
            Machine = LabEvents.CurrentMachine as Pestle;
        }
        public void StopDrag()
        {
            canDrag = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.CompareTag("Smash")) return;
            Debug.LogWarning("Smashed");
            if (LabEvents.CurrentMachine.MachineState != MachineState.Ready)
            {
                LabEvents.OnItemSmashedCall();
            }
        }
    }
}