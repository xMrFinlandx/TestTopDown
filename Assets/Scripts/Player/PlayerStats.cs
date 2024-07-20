using System;
using Entities;
using Scriptables.Entities;
using UnityEngine;
using Utilities.Enums;

namespace Player
{
    public class PlayerStats : MonoBehaviour, IEntityStats
    {
        [SerializeField] private PlayerStatsConfig _playerStatsConfig;
        
        private int _currentHealth;
        
        public static event Action PlayerDiedAction;
        
        public float SpeedModifier { get; private set; }
        public float Speed => _playerStatsConfig.Speed * SpeedModifier;
        public float RotationSpeed => _playerStatsConfig.RotationSpeed;
        
        public EntityType EntityType => EntityType.Player;


        public bool TryApplyDamage(int amount)
        {
            _currentHealth -= amount;
            
            if (_currentHealth <= 0)
                Kill();

            return true;
        }

        public void Kill()
        {
            PlayerDiedAction?.Invoke();
            Destroy(gameObject);
        }

        public void SetSpeedModifier(float value)
        {
            SpeedModifier = value;
        }
        
        private void Start()
        {
            _currentHealth = _playerStatsConfig.MaxHealth;
            SetSpeedModifier(1);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<IDamageable>(out var damageable) && damageable.EntityType == EntityType.Enemy)
                TryApplyDamage(_currentHealth);
        }
    }
}