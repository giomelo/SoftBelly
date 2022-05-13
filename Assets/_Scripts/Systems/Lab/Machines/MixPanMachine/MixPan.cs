using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    public class MixPan : BaseMachine, IMix
    {
        [SerializeField]
        private List<DragObject> decorators = new List<DragObject>();
        [SerializeField]
        private GameObject ingredietnsShelf;
        [SerializeField]
        private Transform pos;
        
        [SerializeField]
        private TextMeshProUGUI decoratorsText;
        [SerializeField]
        private Button resetButton;

        public static Action<IngredientObj> OnIngredientAdd;

        public override void OnEnable()
        {
            base.OnEnable();
            OnIngredientAdd += AddText;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            OnIngredientAdd -= AddText;
        }

        private void AddText(IngredientObj ingredient)
        {
            decoratorsText.text += ingredient.IngredientDescription + "+";
        }

        private void ResetText()
        {
            decoratorsText.text = "";
        }
        public static void OnIngredientAddCall(IngredientObj i)
        {
            OnIngredientAdd?.Invoke(i);
        }
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