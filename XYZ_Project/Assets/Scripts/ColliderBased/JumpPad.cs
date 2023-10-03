using UnityEngine;
namespace Scripts
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _bounceSpeed;



        private Animator _animator;
        private static readonly int IsBounce = Animator.StringToHash("is-bounce");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void OnBounce()
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.AddForce(Vector2.up * _bounceSpeed, ForceMode2D.Impulse);
            _animator.SetTrigger(IsBounce);
        }
    }
}

