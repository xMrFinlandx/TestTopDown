﻿using System;
using Scriptables.Entities;
using UnityEngine;
using Utilities.Enums;

namespace Entities
{
    public class EnemyStats : MonoBehaviour, IEntityStats
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private int _currentHealth;
        
        private EnemyStatsConfig _enemyStatsConfig;
        
        public static event Action<int> EnemyDiedAction;
        
        public float SpeedModifier { get; private set; }
        public float Speed => _enemyStatsConfig.Speed * SpeedModifier;
        
        public EntityType EntityType => EntityType.Enemy;

        public void Init(EnemyStatsConfig enemyStatsConfig, Transform playerTransform)
        {
            _enemyStatsConfig = enemyStatsConfig;
            _spriteRenderer.color = enemyStatsConfig.Color;
        }

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