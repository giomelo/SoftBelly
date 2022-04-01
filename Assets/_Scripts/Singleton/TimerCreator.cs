using UnityEngine;

namespace _Scripts.Singleton
{
    public class TimerCreator : MonoSingleton<TimerCreator>
    {
        [SerializeField]

        private GameObject timerPrefab;
        private void Awake()
        {
            
        }
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(timerPrefab);
            DontDestroyOnLoad(this);
        }
    }
}
