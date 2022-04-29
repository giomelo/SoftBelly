using System;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class Pestle : BaseMachine, IMix
    {
        [SerializeField]
        private PestleObject _pestle;
        public override void CreateResult()
        {
            throw new System.NotImplementedException();
        }

        public void Work()
        {
            _pestle.StartDrag();
            SetState(MachineState.Working);
        }
    }
}
