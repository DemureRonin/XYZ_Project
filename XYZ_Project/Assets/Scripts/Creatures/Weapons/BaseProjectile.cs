using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float ProjectileSpeed;
    [SerializeField] private bool _isFlipped;
    protected Rigidbody2D Rigidbody;
    protected int Direction;

    private void Awake()
    {
        
    }
    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        var mod = _isFlipped ? -1 : 1;
        Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
       
        
    }
}
