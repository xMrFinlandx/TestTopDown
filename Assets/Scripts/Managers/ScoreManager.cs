using Entities;
using UnityEngine;
using Utilities.Classes;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public readonly ReactiveProperty<int> CurrentScoreProperty = new();
        
        private void Start()
        {
            EnemyStats.EnemyDiedAction += OnEnemyDied;
        }

        private void OnEnemyDied(int points)
        {
            CurrentScoreProperty.Value += points;
            print(CurrentScoreProperty.Value);
        }

        private void OnDisable()
        {
            EnemyStats.EnemyDiedAction -= OnEnemyDied;
        }
    }
}