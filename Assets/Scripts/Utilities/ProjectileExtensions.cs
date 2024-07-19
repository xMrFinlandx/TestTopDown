using Gameplay.Projectiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class ProjectileExtensions
    {
        public static T AddSpread<T>(this T projectile, float spread) where T : BaseProjectile
        {
            projectile.Direction = Quaternion.Euler(0, 0, Random.Range(-spread, spread)) * projectile.Direction;
            return projectile;
        }
    }
}