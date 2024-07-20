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
        
        public float SpeedModifier { get; private set; }
        public float Speed => _playerStatsConfig.Speed * SpeedModifier;
        
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
            Destroy(gameObject);
        }

        private void Start()
        {
            _currentHealth = _playerStatsConfig.MaxHealth;
            SetSpeedModifier(1);
        }
        
        public void SetSpeedModifier(float value)
        {
            SpeedModifier = value;
        }
    }
}