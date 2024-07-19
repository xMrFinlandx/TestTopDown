namespace Entities
{
    public interface IDamageable
    {
        public bool TryApplyDamage(int amount);

        public void Kill();
    }
}