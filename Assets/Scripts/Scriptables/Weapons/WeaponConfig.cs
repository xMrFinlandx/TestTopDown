using UnityEngine;
using Utilities.Enums;

namespace Scriptables.Weapons
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;
        [Space] 
        [SerializeField] private EntityType _owner;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _projectileSpeed;
        
        public int Damage => _damage;
        
        public float AttackDelay => 1 / _attackSpeed;
        public float ProjectileSpeed => _projectileSpeed;

        public string Name => _name;
        public string Description => _description;
        
        public EntityType Owner => _owner;
        public Sprite Sprite => _sprite;
        public Color Color => _color;

        public abstract void Attack(Vector2 from, Vector2 to);
    }
}