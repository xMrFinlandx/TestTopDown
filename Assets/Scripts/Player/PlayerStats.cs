using Entities;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;
        
        public bool TryApplyDamage(int amount)
        {
            _currentHealth -= amount;
            
            if (_currentHealth <= 0)
                TryKill();

            return true;
        }

        public bool TryKill()
        {
            Destroy(gameObject);
            return true;
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }
    }
}