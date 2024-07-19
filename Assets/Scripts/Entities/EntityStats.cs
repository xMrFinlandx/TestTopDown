using System;
using UnityEngine;

namespace Entities
{
    public class EntityStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;
        
        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public bool TryApplyDamage(int amount)
        {
            _currentHealth -= amount;
            
            print($"{gameObject.name} take {amount} damage");

            if (_currentHealth <= 0)
                TryKill();

            return true;
        }

        public bool TryKill()
        {
            Destroy(gameObject);
            return true;
        }
    }
}