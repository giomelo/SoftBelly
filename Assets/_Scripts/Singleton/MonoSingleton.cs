using UnityEngine;

namespace _Scripts.Singleton
{
    /// <summary>
    /// Singleton class for make other classes a singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    Debug.LogError(typeof(T).ToString() + " is NULL.");
                }
                return _instance;
            }
        }

        private void Awake() {
            _instance = (T)this;
            Init();
        }

        protected virtual void Init()
        {

        }
    }
}