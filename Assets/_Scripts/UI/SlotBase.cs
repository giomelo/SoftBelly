using System;
using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    /// <summary>
    /// Base slot class for different slots in the game
    /// </summary>
    public abstract class SlotBase : MonoBehaviour
    {
        [SerializeField] 
        public UISlot uiSlot;
        [HideInInspector]
        public UIController Subject;


        private void Start()
        {
            throw new NotImplementedException();
        }

        public void AddSubject(UIController subject)
        {
            Subject = subject;
        }
        
        public void MouseEnter()
        {
            Debug.Log("Enter");
            if (uiSlot.item == null)
            {
                Subject.ResetCurrentProprieties();
                return;
            }

            if (uiSlot.item.ItemProprieties.ItemProprietiesGO == null) return;
            Subject.DisplayCurrentProprieties(uiSlot.item.ItemProprieties.ItemProprietiesGO.gameObject, this.gameObject, uiSlot.item);
           
        }
        
        public void MouseExit()
        {
            Debug.Log("Exit");
            Subject.ResetCurrentProprieties();
        }
    }
}