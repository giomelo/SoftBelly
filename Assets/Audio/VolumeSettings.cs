using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeSettings : MonoBehaviour
{
  [SerializeField] AudioMixer mixer;
  [SerializeField] Slider Music;
  [SerializeField] Slider SFX;

  const string MIXER_MUSIC = "MusicVolume";
  const string MIXER_SFX = "SFXVolume";

  void Awake()
  {
    Music.onValueChanged.AddListener(SetMusicVolume);
    SFX.onValueChanged.AddListener(SetSFXVolume);
  }

  void SetMusicVolume(float value)
  {
    mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);

  }
  void SetSFXVolume(float value)
  {
    mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);

  }
}
