using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    public class SpoonObj : DragObject
    {
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
    }
}