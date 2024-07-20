using Utilities.Enums;

namespace Entities
{
    public interface IDamageable
    {
        public EntityType EntityType { get; }

        public bool TryApplyDamage(int amount);
        public void Kill();
    }
}