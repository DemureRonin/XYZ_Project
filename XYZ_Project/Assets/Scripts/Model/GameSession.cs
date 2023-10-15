using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Utils.Disposables;
using Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckpoint;
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public PlayerData Data => _data;
        private PlayerData _save;
        public QuickInventoryModel QuickInventory { get; private set; }
        private readonly List<string> _checkpoints = new List<string>();
        public PerksModel PerksModel { get; private set; }
        


        private void Awake()
        {
            
            var existSession = GetExistsSession();
            if (existSession != null)
            {
                existSession.StartSession(_defaultCheckpoint);
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
                StartSession(_defaultCheckpoint);
            }
            
        }

        private void StartSession(string defaultCheckpoint)
        {
            SetChecked(defaultCheckpoint);
            LoadHud();
            SpawnHero();
        }

        private void SpawnHero()
        {
            var checkpoints = FindObjectsOfType<CheckpointComponent>();
            var lastCheckPoint = _checkpoints.Last();
            foreach (var checkPoint in checkpoints)
            {
                if (checkPoint.Id == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    break;
                }
            }
        }

       

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(Data);  
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }

        private GameSession GetExistsSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return gameSession;
            }

            return null;
        }
        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();

            _trash.Dispose();
            InitModels();
        }
        

        public bool IsChecked(string id)
        {
            return _checkpoints.Contains(id);
        }

        public void SetChecked(string id)
        {
            if (!_checkpoints.Contains(id))
            {
                Save();
                _checkpoints.Add(id);
                
            }
        }
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}

