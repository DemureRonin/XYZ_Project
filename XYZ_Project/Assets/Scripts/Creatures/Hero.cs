using System;
using Cinemachine;
using Scripts.Components;
using Scripts.Model;
using UnityEditor.Animations;
using UnityEngine;

namespace Scripts
{
    public class Hero : Creature
    {
        [SerializeField] private float _timeToSpawnParticle;
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        [SerializeField] private GameObject[] _potions;

        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private SpawnComponent _throwSpawner;
        [SerializeField] private PlayerDef _playerDef;


        private bool _allowDoubleJump;
        private bool _isDJHappened;
        private float _fallingTime;
        private Animator _animator;
        private GameSession _session;
        private HealthComponent _healthComponent;
        private DamageComponent _healthPotion;
        
        private CinemachineVirtualCamera _vCamera;


        private const string SwordId = "Sword";
        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;
        private bool _canThrow
        {
            get
            {
                if (SelectedItemId == SwordId) return _swordCount > 1;
                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }

        }

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private int _coinCount => _session.Data.Inventory.Count("Coins");
        private int _swordCount => _session.Data.Inventory.Count("Sword");

        protected override void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _animator = GetComponent<Animator>();
            base.Awake();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }
        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            health.SetHealth(_session.Data.Hp.Value);
            UpdateHeroWeapon();
            
            _vCamera = FindObjectOfType<CinemachineVirtualCamera>();
            SetCamera();
        }
        public void SetCamera()
        {
            _vCamera.Follow = FindObjectOfType<Hero>().transform;
        }
        private void OnInventoryChanged(string id, int value)
        {
            if(id == SwordId) UpdateHeroWeapon();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            SpawnFallDust();
        }


        protected override float CalculateYVelocity()
        {
            if (GroundCheck.IsTouchingLayer && _session.PerksModel.IsDoubleJumpSupported) _allowDoubleJump = true;
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {

            if (_allowDoubleJump && !GroundCheck.IsTouchingLayer)
            {
                yVelocity = JumpSpeed;
                Particles.Spawn("Jump");
                Sounds.Play("Jump");

                _isDJHappened = true;
                _allowDoubleJump = false;
                return yVelocity;
            }
            return base.CalculateJumpVelocity(yVelocity);
        }
        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.AddItem(id,value);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (_coinCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_coinCount, 5);
            _session.Data.Inventory.RemoveItem("Coin", numCoinsToDispose);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void SpawnFallDust()
        {
            if (!GroundCheck.IsTouchingLayer)
            {
                _fallingTime += Time.deltaTime;
            }


            if ((_isDJHappened && GroundCheck.IsTouchingLayer) || (_fallingTime > _timeToSpawnParticle && GroundCheck.IsTouchingLayer))
            {
                _isDJHappened = false;
                _fallingTime = 0;
                Particles.Spawn("Fall");
            }
            else if (!_isDJHappened && GroundCheck.IsTouchingLayer && _fallingTime < _timeToSpawnParticle)
                _fallingTime = 0;
        }

       
        public override void Attack()
        {
           
            if (_swordCount <= 0)
            {
                return;
            }
            base.Attack();
            Particles.Spawn("Attack");
        }
        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _swordCount > 0 ? _armed : _disarmed;
        }
        public void OnDoThrow()
        {
            Particles.Spawn("SwordThrow");
            Sounds.Play("Range");

        }

        public void Throw()
        {
          if (_canThrow)
          {
               var throwableId = _session.QuickInventory.SelectedItem.Id;
                var throwableDef = DefsFacade.I.ThrowableItems.Get(throwableId);
               _throwSpawner.SetPrefab(throwableDef.Projectile);


               _animator.SetTrigger(ThrowKey);
               _session.Data.Inventory.RemoveItem(throwableId, 1); 
          }

        }
       

        public void HextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

        public void UsePotion()
        {
            var def = DefsFacade.I.Items.Get(SelectedItemId);
            foreach (var potion in _potions)
            {
                if (def.HasTag(ItemTag.Usable) && potion.tag == def.Id && _session.Data.Hp.Value < _playerDef.MaxHealth)
                {
                    _session.Data.Inventory.RemoveItem(def.Id, 1);
                    _healthPotion = potion.GetComponent<DamageComponent>();
                    Heal(); break;
                }
            }
        }
        public void Heal()
        {
            _healthComponent?.ApplyDamage(_healthPotion.DamageDelta);
            if (_session.Data.Hp.Value > _playerDef.MaxHealth)
                _session.Data.Hp.Value = _playerDef.MaxHealth;
            Particles.Spawn("UsePotion");
        }
    }
}
