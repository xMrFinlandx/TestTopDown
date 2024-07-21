using Player;
using UnityEngine;

namespace Scriptables.StatusEffects
{
    public abstract class StatusEffectConfig : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;
        [SerializeField] private float _lifeTime = 10;
        
        public float LifeTime => _lifeTime;

        public Sprite Sprite => _sprite;
        public Color Color => _color;
        
        public abstract void OnApply(PlayerStats playerStats);
        public abstract void OnRemove(PlayerStats playerStats);
    }
}