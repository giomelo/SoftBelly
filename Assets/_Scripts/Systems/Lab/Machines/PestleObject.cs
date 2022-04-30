using System;
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
        private Pestle machine;
        public void StartDrag()
        {
            canDrag = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Smash"))
            {
                Debug.LogWarning("Smashed");
            }
        }
    }
}