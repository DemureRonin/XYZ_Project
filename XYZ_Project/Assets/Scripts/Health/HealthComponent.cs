using System;
using Scripts.Model;
using UnityEngine;
using UnityEngine.Events;
namespace Scripts 
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;
        private GameSession _session;

        private void Awake()
        {
            _session = FindObjectOfType<GameSession>();
        }
        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _onChange?.Invoke(_health);
            if (damageValue > 0 )
            _onDamage?.Invoke();
             if (_health <= 0) _onDie?.Invoke();

           

        }

        public void SetHealth(int health)
        {
            _health = health;
        }


    }
    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {

    }
    

}

