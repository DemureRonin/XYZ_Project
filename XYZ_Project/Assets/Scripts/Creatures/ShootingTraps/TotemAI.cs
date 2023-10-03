using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAI : ShooterTrapAI
{
    [SerializeField] private TotemFOV _totemStructure;

    public int totemPosition;
   protected override void Awake()
   {
        base.Awake();
   }
    
    public void  ShootProjectile()
    {
        Animator.SetTrigger(Range);   
    }
    public void OnTotemDestroy()
    {
        _totemStructure.currentTotemDestroyed = totemPosition;
    }
}
