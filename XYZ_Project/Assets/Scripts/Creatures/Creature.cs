using Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Creature : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private bool _invertScale;
    [SerializeField] protected float Speed;
    [SerializeField] protected float JumpSpeed;
    [SerializeField] protected float DamageSpeed;
    [SerializeField] protected int Damage;

    [Header("Checkers")]
    [SerializeField] protected LayerMask InteractionLayer;
    [SerializeField] protected LayerCheck GroundCheck;
    [SerializeField] protected CheckCircleOverlap[] AttackRange;
    [SerializeField] protected SpawnListComponent Particles;

    protected PlaySoundsComponent Sounds;
    protected Vector2 Direction;
    protected Rigidbody2D Rigidbody;
    protected bool IsJumping;
    protected Animator Animator;
    
    private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int AttackAnimation = Animator.StringToHash("attack");

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Sounds = GetComponent<PlaySoundsComponent>();   
    }
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
    protected virtual void FixedUpdate()
    {
        var isGrounded = GroundCheck.IsTouchingLayer;
        
        var xVelocity = Direction.x * Speed;
        var yVelocity = CalculateYVelocity();
       
        Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
        Animator.SetBool(IsGroundKey, isGrounded);
        Animator.SetBool(IsRunning, Direction.x != 0);
        UpdateSpriteDirection(Direction);
    }
    protected virtual float CalculateYVelocity()
    {
        var yVelocity = Rigidbody.velocity.y;
        
        var isJumpingStart = !IsJumping && Direction.y > 0 && GroundCheck.IsTouchingLayer;
        IsJumping = Direction.y > 0;
        if (IsJumping)
        {
            var isFalling = Rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;
            yVelocity = CalculateJumpVelocity(yVelocity);
            if (isJumpingStart)
            {
                Rigidbody.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
                Sounds.Play("Jump");


            }

        }

        return yVelocity;
    }
    protected virtual float CalculateJumpVelocity(float yVelocity)
    {
        
        if (GroundCheck.IsTouchingLayer)
        {
            Particles.Spawn("Jump");
            yVelocity += JumpSpeed;
        }
        
        return yVelocity;
    }

    public void UpdateSpriteDirection(Vector2 direction)
    {
        var multiplier = _invertScale? -1: 1;
        if (Direction.x > 0)
        {
            transform.localScale = new Vector3(multiplier, 1, 1);

        }
        else if (Direction.x < 0)
        {
            transform.localScale = new Vector3(-1*multiplier, 1, 1);


        }
    }
     public virtual void TakeDamage()
    {
        Animator.SetTrigger(Hit);
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, DamageSpeed);
    }
    public virtual void Attack()
    {
        
        Animator.SetTrigger(AttackAnimation);
      

    }
    public void AttackHit()
    {
        for (int i = 0; i < AttackRange.Length; i++)
        {
            AttackRange[i].Check();

        }


        Sounds.Play("Melee");


    }
}
