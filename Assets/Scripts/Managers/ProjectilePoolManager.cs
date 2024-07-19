using Gameplay.Projectiles;
using UnityEngine;
using UnityEngine.Pool;
using Utilities;

namespace Managers
{
    public class ProjectilePoolManager : Singleton<ProjectilePoolManager>
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private DistanceLimitedBullet _distanceLimitedBullet;

        private ObjectPool<Bullet> _bulletsPool;
        private ObjectPool<DistanceLimitedBullet> _distanceLimitedBulletsPool;

        public DistanceLimitedBullet GetDistanceLimitedBullet(Vector2 from, Vector2 to, float speed, int damage)
        {
            return InternalGet(_distanceLimitedBulletsPool, from, to, speed, damage);
        }
        
        public Bullet GetBullet(Vector2 from, Vector2 to, float speed, int damage)
        {
            return InternalGet(_bulletsPool, from, to, speed, damage);
        }

        public void Release<T>(T projectile) where T : BaseProjectile
        {
            if (projectile is Bullet bullet)
            {
                InternalRelease(_bulletsPool, bullet);
            }
            else if (projectile is DistanceLimitedBullet dlBullet)
            {
                InternalRelease(_distanceLimitedBulletsPool, dlBullet);
            }
        }

        private static void InternalRelease<T>(IObjectPool<T> pool, T projectile) where T : BaseProjectile
        {
            projectile.gameObject.SetActive(false);
            pool.Release(projectile);
        }

        private static T InternalGet<T>(IObjectPool<T> pool, Vector2 from, Vector2 to, float speed, int damage) where T : BaseProjectile
        {
            var projectile = pool.Get();
            projectile.transform.position = from;
            projectile.gameObject.SetActive(true);
            projectile.Initialize(from, to, speed, damage);
            return projectile;
        }

        private void Start()
        {
            _bulletsPool = new ObjectPool<Bullet>(CreateBullet);
            _distanceLimitedBulletsPool = new ObjectPool<DistanceLimitedBullet>(CreateDistanceLimitedBullet);
        }

        private DistanceLimitedBullet CreateDistanceLimitedBullet() => Instantiate(_distanceLimitedBullet, transform);

        private Bullet CreateBullet() => Instantiate(_bullet, transform);
    }
}