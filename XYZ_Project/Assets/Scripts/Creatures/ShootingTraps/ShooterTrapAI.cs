using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class ShooterTrapAI : MonoBehaviour
{
    [SerializeField] private CheckCircleOverlap _meleeAttack;
    [SerializeField] protected LayerCheck FOV;

    [SerializeField] protected SpawnComponent RangeProjectile;
    [SerializeField] private LayerCheck _meleeCanAttack;

    [SerializeField] private float _meleeCooldown;
    [SerializeField] protected float _rangeCooldown;

    private Coroutine _currentRoutine;

    private static readonly int Melee = Animator.StringToHash("melee");
    protected static readonly int Range = Animator.StringToHash("shoot");


    protected Animator Animator;
   

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    protected void StartState(IEnumerator coroutine)
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);
        _currentRoutine = StartCoroutine(coroutine);
    }
    
    public virtual void OnHeroInVision()
    {
        if (FOV.IsTouchingLayer)
        {
            if (_meleeCanAttack.IsTouchingLayer)
            {
                StartState(MeleeAttack());
                return;
            }
            StartState(RangeAttack());
        }
    }
    public virtual IEnumerator RangeAttack()
    {
        while  (FOV.IsTouchingLayer && !_meleeCanAttack.IsTouchingLayer)
        {
            Animator.SetTrigger(Range);
            yield return new WaitForSeconds(_rangeCooldown);
        }
        if (_meleeCanAttack.IsTouchingLayer)
        StartState(MeleeAttack());
    }


    IEnumerator  MeleeAttack()
    {
        while (_meleeCanAttack.IsTouchingLayer)
        {
            Animator.SetTrigger(Melee);
            yield return new WaitForSeconds(_meleeCooldown);
        }
        if (FOV.IsTouchingLayer && !_meleeCanAttack.IsTouchingLayer)
        StartState(RangeAttack());
    }
    public void OnMeleeAttack()
    {
        _meleeAttack.Check();
    }
    public void OnRangeAttack()
    {
        RangeProjectile.Spawn();
    }
}
