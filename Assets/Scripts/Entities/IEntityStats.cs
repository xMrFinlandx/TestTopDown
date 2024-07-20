namespace Entities
{
    public interface IEntityStats : IDamageable
    {
        public float SpeedModifier { get; }
        public float Speed { get; }

        public void SetSpeedModifier(float value);
    }
}