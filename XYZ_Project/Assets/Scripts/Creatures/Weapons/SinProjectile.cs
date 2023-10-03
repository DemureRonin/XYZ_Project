using UnityEngine;

public class SinProjectile : BaseProjectile
{
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;
    private float _originalY;
    private float _time;
    protected override void Start()
    {
        base.Start();
        _originalY = Rigidbody.position.y;
    }
    private void FixedUpdate()
    {
        var position = Rigidbody.position; 
        position.x += Direction * ProjectileSpeed;
        position.y = _originalY + Mathf.Sin(_time* _frequency) * _amplitude;
        Rigidbody.MovePosition(position);
        _time += Time.fixedDeltaTime;
    }
}
