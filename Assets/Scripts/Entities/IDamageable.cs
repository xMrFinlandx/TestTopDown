using Utilities.Enums;

namespace Entities
{
    public interface IDamageable
    {
        public EntityType EntityType { get; }

        public void TryApplyDamage(int amount);
        public void Kill();
    }
}