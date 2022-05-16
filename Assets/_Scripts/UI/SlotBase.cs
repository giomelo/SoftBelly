﻿using _Scripts.Enums;
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

        protected UIController _subject;


        public void AddSubject(UIController subject)
        {
            _subject = subject;
        }
        
        public void MouseEnter()
        {
            if (uiSlot.item == null)
            {
                _subject.ResetCurrentProprieties();
                return;
            }

            if (uiSlot.item.ItemProprieties.ItemProprietiesGO == null) return;
            _subject.DisplayCurrentProprieties(uiSlot.item.ItemProprieties.ItemProprietiesGO.gameObject, this.transform, uiSlot.item);
           
        }
        
        public void MouseExit()
        {
            _subject.ResetCurrentProprieties();
        }
    }
}