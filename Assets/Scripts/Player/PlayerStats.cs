using System;
using System.Collections.Generic;
using Entities;
using Scriptables.Entities;
using Scriptables.StatusEffects;
using UnityEngine;
using Utilities.Classes;
using Utilities.Enums;

namespace Player
{
    public class PlayerStats : MonoBehaviour, IEntityStats
    {
        [SerializeField] private PlayerStatsConfig _playerStatsConfig;
        [SerializeField] private WeaponSelector _weaponSelector;
        
        private int _currentHealth;

        private float _invisibilityCounter;
        private float _statusEffectSpeedModifier;

        private readonly List<StatusEffectHolder> _statusEffects = new();
        
        public static event Action PlayerDiedAction;
        
        public float SpeedModifier { get; private set; }
        public float Speed => _playerStatsConfig.Speed * SpeedModifier * _statusEffectSpeedModifier;
        public float RotationSpeed => _playerStatsConfig.RotationSpeed;
        
        public WeaponSelector WeaponSelector => _weaponSelector;
        public EntityType EntityType => EntityType.Player;
        
        public void TryApplyDamage(int amount)
        {
            if (_invisibilityCounter > 0)
                return;
            
            _currentHealth -= amount;
            
            if (_currentHealth <= 0)
                Kill();
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

        public void SetStatusEffectSpeedModifier(float value)
        {
            _statusEffectSpeedModifier = value;
        }

        public void SetInvisibility(float invisibility)
        {
            _invisibilityCounter = invisibility;
        }

        public void ApplyStatusEffect(StatusEffectConfig statusEffect)
        {
            var effect = new StatusEffectHolder(statusEffect, this);
            effect.Apply();
            _statusEffects.Add(effect);
        }

        private void Start()
        {
            _currentHealth = _playerStatsConfig.MaxHealth;
            SetSpeedModifier(1);
            SetStatusEffectSpeedModifier(1);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<IDamageable>(out var damageable) && damageable.EntityType == EntityType.Enemy)
                TryApplyDamage(_currentHealth);
        }

        private void Update()
        {
            if (_invisibilityCounter > 0)
                _invisibilityCounter -= Time.deltaTime;

            if (_statusEffects == null)
                return;

            for (var i = 0; i < _statusEffects.Count; i++)
            {
                var statusEffect = _statusEffects[i];
                statusEffect.Update(Time.deltaTime);

                if (!statusEffect.CanRemove)
                    continue;

                statusEffect.OnRemove();
                _statusEffects.Remove(statusEffect);
            }
        }
    }
}