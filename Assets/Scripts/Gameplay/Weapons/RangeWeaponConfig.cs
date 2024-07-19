using Managers;
using UnityEngine;

namespace Gameplay.Weapons
{
    [CreateAssetMenu(fileName = "New Range Weapon Data", menuName = "Weapons/Range Weapon Data", order = 0)]
    public class RangeWeaponConfig : WeaponConfig
    {
        public override void Attack(Vector2 from, Vector2 to)
        {
            ProjectilePoolManager.Instance.GetBullet(from, to, ProjectileSpeed, Damage);
        }
    }
}