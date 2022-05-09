using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class MixPan : BaseMachine, IMix
    {
        [SerializeField]
        private List<DragObject> decorators = new List<DragObject>();
        [SerializeField]
        private GameObject ingredietnsShelf;
        [SerializeField]
        private Transform pos;
        public override void CreateResult()
        {
            throw new System.NotImplementedException();
        }
        
        protected override void InitMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraMix();
            ingredietnsShelf.SetActive(true);
        }
        protected override void FinishMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraMix();
            OnDisposeMachine();
            ingredietnsShelf.SetActive(false);
        }

        private void OnDisposeMachine()
        {
            
        }

        public void Work()
        {
            if (MachineState is MachineState.Working or MachineState.Ready) return;
            
            foreach (var ingredient in decorators)
            {
                ingredient.StartDrag();
            }
            SetState(MachineState.Working);
            Create();
        }
        
        private void Create()
        {
            
        }
    }
}