using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Helpers
{
    /// <summary>
    /// Basic audio system
    /// </summary>
    public class AudioSystem : MonoSingleton<AudioSystem>
    {
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource soundSource;

        public void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        
        public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1)
        {
            soundSource.clip = clip;
            PlaySound(clip, vol);
        }

        public void PlaySound(AudioClip clip, float vol = 1)
        {
            soundSource.PlayOneShot(clip, vol);
        }
    }
}