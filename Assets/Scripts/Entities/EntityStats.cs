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
                Kill();

            return true;
        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}