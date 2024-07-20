using UnityEngine;

namespace Scriptables.Entities
{
    public abstract class EntityStatsConfig : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _speed;

        public int MaxHealth => _maxHealth;
        public float Speed => _speed;
    }
}