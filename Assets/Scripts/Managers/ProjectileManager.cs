using Gameplay.Projectiles;
using UnityEngine;
using UnityEngine.Pool;
using Utilities;

namespace Managers
{
    public class ProjectileManager : Singleton<ProjectileManager>
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private DistanceLimitedBullet _distanceLimitedBullet;

        private ObjectPool<Bullet> _bulletsPool;
        private ObjectPool<DistanceLimitedBullet> _distanceLimitedBulletsPool;

        public DistanceLimitedBullet GetDistanceLimitedBullet(Vector2 from, Vector2 to, float speed, int damage)
        {
            var bullet = _distanceLimitedBulletsPool.Get();
            bullet.transform.position = from;
            bullet.gameObject.SetActive(true);
            bullet.Initialize(from, to, speed, damage);
            return bullet;
        }

        public void Release(DistanceLimitedBullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _distanceLimitedBulletsPool.Release(bullet);
        }
        
        public Bullet GetBullet(Vector2 from, Vector2 to, float speed, int damage)
        {
            var bullet = _bulletsPool.Get();
            bullet.transform.position = from;
            bullet.gameObject.SetActive(true);
            bullet.Initialize(from, to, speed, damage);
            return bullet;
        }

        public void Release(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _bulletsPool.Release(bullet);
        }

        private void Start()
        {
            _bulletsPool = new ObjectPool<Bullet>(CreateBullet);
            _distanceLimitedBulletsPool = new ObjectPool<DistanceLimitedBullet>(CreateDistanceLimitedBullet);
        }

        private DistanceLimitedBullet CreateDistanceLimitedBullet()
        {
            return Instantiate(_distanceLimitedBullet, transform);
        }

        private Bullet CreateBullet()
        {
            return Instantiate(_bullet, transform);
        }
    }
}