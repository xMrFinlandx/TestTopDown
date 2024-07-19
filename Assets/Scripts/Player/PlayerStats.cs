using Entities;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour, IEntityStats
    {
        [SerializeField] private int _maxHealth;

        public float SpeedModifier { get; private set; } = 1;
        
        private int _currentHealth;
        
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
            _currentHealth = _maxHealth;
        }
        
        public void SetSpeedModifier(float value)
        {
            SpeedModifier = value;
        }
    }
}