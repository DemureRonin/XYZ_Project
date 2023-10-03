using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(AudioSource))]
public class AudioSettingComponent : MonoBehaviour
{
    [SerializeField] private SoundSetting _mode;
     private AudioSource _audioSource;
    private FloatPersistentProperties _model;
    private void Start()
    {
        _model = FindProperty();
        _model.OnChanged += OnSoundsSettingChanged;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnSoundsSettingChanged(float newValue, float oldValue)
    {
        _audioSource.volume = newValue; 
    }

    private FloatPersistentProperties FindProperty()
    {
        switch (_mode)
        {
            case SoundSetting.Music:
                return GameSettings.I.Music;
            case SoundSetting.Sfx:
                return GameSettings.I.Sfx;

        }
        throw new ArgumentException("Unidentified mode");
    }
}
