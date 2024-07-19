using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [SerializeField] private string _description;
        [SerializeField] private string _name;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _projectileSpeed;
        
        public int Damage => _damage;
        public float AttackDelay => 1 / _attackSpeed;
        public float ProjectileSpeed => _projectileSpeed;

        public abstract void Attack(Vector2 from, Vector2 to);
    }
}