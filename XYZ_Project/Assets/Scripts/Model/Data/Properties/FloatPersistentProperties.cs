﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class FloatPersistentProperties : PrefsPersistentProperties<float>
{
    public FloatPersistentProperties(float defaultValue, string key) : base(defaultValue, key)
    {
        Init();

    }
    protected override void Write(float value)
    {
        PlayerPrefs.SetFloat(Key, value);
        PlayerPrefs.Save();
    }
    protected override float Read(float defaultValue)
    {
        return PlayerPrefs.GetFloat(Key, defaultValue);
    }
}