﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class PersistentProperties<TPropertyType>
{
    [SerializeField] protected TPropertyType _value;
    private TPropertyType _defaultValue;
    protected TPropertyType _stored;
    public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
    public event OnPropertyChanged OnChanged;

    public PersistentProperties(TPropertyType defaultValue)
    {
        _defaultValue = defaultValue;
    }
    public TPropertyType Value
    {
        get => _stored;
        set
        {
            var isEquals = _stored.Equals(value);
            if (isEquals) return;

            var oldValue = _value;
            Write(value);
            _stored =  _value = value;

            OnChanged?.Invoke(value, oldValue);
        }
    }
    protected void Init()
    {
        _stored =  _value = Read(_defaultValue);
    }
    protected abstract void Write(TPropertyType value);
    protected abstract TPropertyType Read(TPropertyType defaultValue);

    public void Validate()
    {
        if (!_stored.Equals(_value))
            Value = _value;
    }
}
