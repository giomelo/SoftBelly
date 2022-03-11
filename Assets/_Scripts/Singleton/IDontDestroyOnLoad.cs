using UnityEngine;

namespace _Scripts.Singleton
{
    public interface IDontDestroyOnLoad<T>
    {
        public static T _instance;
    }
}