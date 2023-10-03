using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
  
    
    protected override void Start()
    {
        base.Start();
        var force = new Vector2(Direction * ProjectileSpeed, 0);
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
    
}
