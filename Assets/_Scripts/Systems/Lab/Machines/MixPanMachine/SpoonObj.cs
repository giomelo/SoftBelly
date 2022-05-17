using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    public class SpoonObj : DragObject
    {
        [SerializeField]
        private MixPan Machine;
        public override void StartDrag()
        {
            CanDrag = true;
        }

        public override void Start()
        {
            base.Start();
            CanDrag = true;
            rb = GetComponent<Rigidbody>();
        }
        public override void OnMouseUp()
        {
            base.OnMouseUp();
            rb.isKinematic = true;
            StartCoroutine(BackRigidbody());
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.CompareTag("Mixed")) return;
            if (!Machine.CanHit) return;
            Debug.LogWarning("Smashed");
            if (LabEvents.CurrentMachine.MachineState != MachineState.Ready)
            {
                LabEvents.OnItemMixedCall();
                StartCoroutine(Machine.ResetCoolDown());
            }
        }
    }
}