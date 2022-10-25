using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Enums.GameEventsSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] 
        private GameEvent gameEvent;
        [SerializeField] 
        private UnityEvent unityEvent;

        private void Awake() => gameEvent.Register(this);
        private void OnDestroy() => gameEvent.Deregister(this);
        
        public void RaiseEvent() => unityEvent.Invoke();
    }
}