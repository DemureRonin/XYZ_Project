﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;
    [SerializeField] private bool randomize;
    private float _originalY;
    private Rigidbody2D _rigidbody;
    private float _seed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalY = _rigidbody.position.y;
        if (randomize)
        {
            _seed = Random.value * Mathf.PI * 2;
        }
    }
    private void Update()
    {
        var pos = _rigidbody.position;
        pos.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
        _rigidbody.MovePosition(pos);
    }
}
