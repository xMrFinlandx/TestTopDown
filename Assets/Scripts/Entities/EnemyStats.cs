using System;
using Scriptables.Entities;
using UnityEngine;
using Utilities.Enums;

namespace Entities
{
    public class EnemyStats : MonoBehaviour, IEntityStats
    {
        private EnemyStatsConfig _enemyStatsConfig;
        
        private int _currentHealth;
        public static event Action<int> EnemyDiedAction;
        
        public float SpeedModifier { get; private set; }
        public float Speed => _enemyStatsConfig.Speed * SpeedModifier;
        
        public EntityType EntityType => EntityType.Enemy;

        public void Init(EnemyStatsConfig enemyStatsConfig) => _enemyStatsConfig = enemyStatsConfig;
        
        public bool TryApplyDamage(int amount)
        {
            _currentHealth -= amount;
            
            if (_currentHealth <= 0)
                Kill();

            return true;
        }

        public void Kill()
        {
            EnemyDiedAction?.Invoke(_enemyStatsConfig.Points);
            Destroy(gameObject);
        }
        
        public void SetSpeedModifier(float value)
        {
            SpeedModifier = value;
        }
        
        private void Start()
        {
            _currentHealth = _enemyStatsConfig.MaxHealth;
            SetSpeedModifier(1);
        }
    }
}