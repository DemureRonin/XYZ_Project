﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    public void DestroyObject()
    {
        Destroy(_gameObject);
    }
}
