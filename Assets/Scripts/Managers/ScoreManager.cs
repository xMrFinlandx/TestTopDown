using System;
using Entities;
using Managers.Queue;
using Player;
using Systems.Save;
using Systems.Save.Data;
using Utilities.Classes;

namespace Managers
{
    public class ScoreManager : QueueElement, IDataPersistence
    {
        public static readonly ReactiveProperty<int> CurrentScoreProperty = new();

        public static event Action<int, int> ScoreSavedAction; 
        
        public override void Enable()
        {
            CurrentScoreProperty.Value = 0;
        }
        
        public void LoadData(GameData data)
        {
        }

        public void SaveData(GameData data)
        {
            ScoreSavedAction?.Invoke(data.BestScore, CurrentScoreProperty.Value);
            
            if (CurrentScoreProperty.Value > data.BestScore)
                data.BestScore = CurrentScoreProperty.Value;
        }
        
        private void Start()
        {
            DataPersistenceManager.Load();
            
            PlayerStats.PlayerDiedAction += OnPlayerDied;
            EnemyStats.EnemyDiedAction += OnEnemyDied;
        }

        private void OnPlayerDied()
        {
            DataPersistenceManager.Save();
        }

        private void OnEnemyDied(EnemyStats enemyStats)
        {
            CurrentScoreProperty.Value += enemyStats.Points;
        }

        private void OnDisable()
        {
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
            EnemyStats.EnemyDiedAction -= OnEnemyDied;
        }
    }
}