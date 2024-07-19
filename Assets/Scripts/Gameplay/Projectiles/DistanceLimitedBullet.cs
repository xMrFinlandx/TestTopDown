using Managers;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class DistanceLimitedBullet : BaseProjectile
    {
        private float _maxDistance;
        private Vector2 _from;

        public DistanceLimitedBullet SetMaxDistance(float maxDistance)
        {
            _maxDistance = maxDistance;
            return this;
        }

        public override void Initialize(Vector2 from, Vector2 to, float speed, int damage)
        {
            _from = from;
            base.Initialize(from, to, speed, damage);
        }

        protected override void ReleaseProjectile()
        {
            OnRelease();
            ProjectilePoolManager.Instance.Release(this);
        }

        private void FixedUpdate()
        {
            Move();
            
            var travelledDistance = Vector2.Distance(_from, transform.position);

            if (travelledDistance >= _maxDistance)
                ReleaseProjectile();
        }
    }
}