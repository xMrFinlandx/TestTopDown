using Entities;
using Player;
using Systems.Save;
using Systems.Save.Data;
using UnityEngine;
using Utilities.Classes;

namespace Managers
{
    public class ScoreManager : MonoBehaviour, IDataPersistence
    {
        public readonly ReactiveProperty<int> CurrentScoreProperty = new();
        
        public void LoadData(GameData data)
        {
            Debug.Log($"Loaded {data.BestScore}");
        }

        public void SaveData(GameData data)
        {
            if (CurrentScoreProperty.Value > data.BestScore)
                data.BestScore = CurrentScoreProperty.Value;
            
            Debug.Log($"Saved {data.BestScore}, current {CurrentScoreProperty.Value}");
        }
        
        private void Start()
        {
            EnemyStats.EnemyDiedAction += OnEnemyDied;
            PlayerStats.PlayerDiedAction += OnPlayerDied;
            
            DataPersistenceManager.Load();
        }

        private void OnPlayerDied()
        {
            DataPersistenceManager.Save();
        }

        private void OnEnemyDied(int points)
        {
            CurrentScoreProperty.Value += points;
        }

        private void OnDisable()
        {
            EnemyStats.EnemyDiedAction -= OnEnemyDied;
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
        }
    }
}