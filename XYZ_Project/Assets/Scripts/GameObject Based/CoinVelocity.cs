using UnityEngine;
namespace Scripts
{
    public class CoinVelocity : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _coinRigidbody;
        [SerializeField] private float _coinSpeed;


        private void Start()
        {
            _coinRigidbody.AddForce(Vector2.up * _coinSpeed, ForceMode2D.Impulse);
        }
    }

}
