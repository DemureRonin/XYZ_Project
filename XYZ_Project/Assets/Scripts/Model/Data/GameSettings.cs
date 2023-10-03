using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject 
{
    [SerializeField] private FloatPersistentProperties _music;
    [SerializeField] private FloatPersistentProperties _sfx;
    public FloatPersistentProperties Music => _music;
    public FloatPersistentProperties Sfx => _sfx;


    private static GameSettings _instance;
    public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

    private static GameSettings LoadGameSettings()
    {
        return _instance = Resources.Load<GameSettings>("GameSettings");
    }

    private void OnEnable()
    {
        _music = new FloatPersistentProperties(1, SoundSetting.Music.ToString());
        _sfx =  new FloatPersistentProperties(1, SoundSetting.Sfx.ToString());
    }
    private void OnValidate()
    {
        Music.Validate();
        Sfx.Validate();
    }
 
}
public enum SoundSetting
{
    Music,
    Sfx
}
