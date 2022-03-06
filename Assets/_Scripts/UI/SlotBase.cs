using UnityEngine;

namespace _Scripts.UI
{
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