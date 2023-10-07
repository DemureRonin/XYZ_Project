using System;
using Scripts.Model;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.HUD
{
    public class CoinHud : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private int _value;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            
            _text.text =  $"x{_session.Data.Inventory.Count("Coin").ToString()}";
        }

        

        public void OnValueChanged()
        {
            if (_session == null) return;
            _text.text = $"x{_session.Data.Inventory.Count("Coin").ToString()}";
            
        }
    }
}