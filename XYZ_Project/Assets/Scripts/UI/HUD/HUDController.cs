using Model;
using Model.Definitions;
using Scripts.Model;
using UI.Widgets;
using UnityEngine;


    public class HUDController : MonoBehaviour
    {
        [SerializeField] private ProgressBar _healthBar;
        private GameSession _gameSession;
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _gameSession.Data.Hp.OnChanged += OnHealthChanged;
            OnHealthChanged(_gameSession.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }
        private void OnDestroy()
        {
            _gameSession.Data.Hp.OnChanged -= OnHealthChanged;
        }

    }

