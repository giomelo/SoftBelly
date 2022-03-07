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
    }
}