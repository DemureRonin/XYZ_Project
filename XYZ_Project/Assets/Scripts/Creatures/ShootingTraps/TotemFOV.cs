using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemFOV : ShooterTrapAI
{
    [SerializeField] private GameObject _selfDestroy;
    [SerializeField] private List<TotemAI> _totems;

    [NonSerialized] public int currentTotemDestroyed;
    

    private void Start()
    {
        for (int i =0;  i < _totems.Count; i++)
        {
            _totems[i].totemPosition = i;
        }
    }
    public override void OnHeroInVision()
    {
        if (FOV.IsTouchingLayer)
        {
            StartCoroutine(StartAttack());
        }

    }
    private IEnumerator StartAttack()
    {
        while (FOV.IsTouchingLayer && _totems.Count !=0)
        {
            for (int i = 0; i < _totems.Count; i++)
            {
                if (FOV.IsTouchingLayer)
                {
                    _totems[i].ShootProjectile();
                    if (i == _totems.Count)
                    {
                        i = 0;
                    }
                    yield return new WaitForSeconds(_rangeCooldown);
                }
            }
        }
    }
    public void ChangeOnDestroy()
    {
        _totems.RemoveAt(_totems[currentTotemDestroyed].totemPosition);
        if (_totems.Count == 0)
        {
            Destroy(_selfDestroy);
        }
    }
}

