using Managers;

namespace Gameplay.Projectiles
{
    public class Bullet : BaseProjectile
    {
        protected override void ReleaseProjectile()
        {
            OnRelease();
            ProjectilePoolManager.Instance.Release(this);
        }
        
        private void FixedUpdate()
        {
            Move();
        }
    }
}