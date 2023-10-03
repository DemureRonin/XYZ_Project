using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrefsPersistentProperties<TPopertyType> : PersistentProperties<TPopertyType> 
{
    protected string Key;
    protected PrefsPersistentProperties(TPopertyType defaultValue, string key) : base(defaultValue)
    {
        Key = key;
    }
}
