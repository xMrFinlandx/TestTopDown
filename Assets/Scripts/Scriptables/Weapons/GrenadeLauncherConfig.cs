using Entities;
using Managers;
using UnityEngine;
using Utilities;

namespace Scriptables.Weapons
{
    [CreateAssetMenu(fileName = "New Grenade Launcher Data", menuName = "Weapons/Grenade Launcher Data", order = 0)]
    public class GrenadeLauncherConfig : WeaponConfig
    {
        [Space] 
        [SerializeField] private float _explosionRadius = 2f;
        [SerializeField] private LayerMask _layerMask;

        public override void Attack(Vector2 from, Vector2 to)
        {
            ProjectilePoolManager.Instance.GetDistanceLimitedBullet(from, to, ProjectileSpeed, Damage)
                .SetMaxDistance(Vector2.Distance(from, to))
                .IgnoreCollisions()
                .SetOnDestroyAction(pos =>
                {
                    OverlapHelper.GetComponentsInCircle<IDamageable>(pos, _explosionRadius, _layerMask)
                        .ForEach(damageable => damageable.TryApplyDamage(Damage));
                });
        }
    }
}