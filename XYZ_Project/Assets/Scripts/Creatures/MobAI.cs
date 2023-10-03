using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _FOV;
    [SerializeField] private LayerCheck _canAttack;
    [SerializeField] private float _attackCooldown = 2f;

    private IEnumerator _currentRoutine;
    private Creature _creature;
    private Patrol _patrol;

    private SpawnListComponent _particles;
    private Animator _animator;
    private static readonly int DieKey = Animator.StringToHash("die");

    [SerializeField] private float _alarmDelay = 0.5f;
    private GameObject _target;
    private bool _isDead;

    private void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartState(_patrol.DoPatrol());
    }
    public void OnHeroInVision(GameObject gameObject)
    {
        if(_isDead) return;
        
            _target = gameObject;
            StartState(AgroToHero());
        
        
    }

    private IEnumerator AgroToHero()
    {
        LookAtHero();
        _particles.Spawn("Exclamation");
        yield return new WaitForSeconds (_alarmDelay);
       StartState(GoToHero());
    }

    private void LookAtHero()
    {
        var direction = GetDirection();

        _creature.SetDirection(Vector2.zero);

        _creature.UpdateSpriteDirection(direction);
    }
    private Vector2 GetDirection()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        return direction.normalized;
    }

    private IEnumerator GoToHero()
    {

        while (_FOV.IsTouchingLayer)
        {
            if (_canAttack.IsTouchingLayer)
            {
                StartState(Attack());
              
            }
            else
            {
                SetDirectionToTarget();

            }
            yield return null;
        }
        _creature.SetDirection(Vector2.zero);
        _particles.Spawn("Lose");
        yield return new WaitForSeconds(2f);
        StartState(_patrol.DoPatrol());
    }

    private IEnumerator Attack()
    {
        while (_canAttack.IsTouchingLayer)
        {
            _creature.Attack();
            
            yield return new WaitForSeconds (_attackCooldown) ;

        }
        StartState(GoToHero());
    }

    private void SetDirectionToTarget()
    {
        var direction = GetDirection();
        _creature.SetDirection(direction.normalized);
    }

   
    private void StartState(IEnumerator coroutine)
    {
        
        _creature.SetDirection(Vector2.zero);
        if(_currentRoutine != null)
            StopCoroutine(_currentRoutine);
        _currentRoutine = coroutine;
        StartCoroutine(coroutine);   
    }
    public void OnDie()
    {
        _isDead = true;
        _animator.SetBool(DieKey, true);
        _creature.SetDirection(Vector2.zero);

        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);
    }

    
    
}
