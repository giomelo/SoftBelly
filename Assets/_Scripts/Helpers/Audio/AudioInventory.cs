using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Helpers.Audio
{
    public class AudioInventory : MonoSingleton<AudioInventory>
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public AudioClip rightAudio;
        public AudioClip wrongAudio;
        public AudioClip wonSound;
        public AudioClip looseSound;
        public AudioClip walkSound;
    }
}