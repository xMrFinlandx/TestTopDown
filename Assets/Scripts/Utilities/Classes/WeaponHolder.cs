using Scriptables.Weapons;
using UnityEngine;

namespace Utilities.Classes
{
    public class WeaponHolder
    {
        private readonly float _attackDelay;
        private float _attackDelayCounter;

        public WeaponConfig WeaponConfig { get; }

        public bool CanAttack => _attackDelayCounter <= 0;
        
        public WeaponHolder(WeaponConfig weaponConfig)
        {
            WeaponConfig = weaponConfig;
            _attackDelay = weaponConfig.AttackDelay;
            _attackDelayCounter = _attackDelay;
        }

        public void Update(float deltaTime)
        {
            _attackDelayCounter -= deltaTime;
        }

        public void Attack(Vector2 from, Vector2 to)
        {
            _attackDelayCounter = _attackDelay;
            WeaponConfig.Attack(from, to);
        }
    }
}