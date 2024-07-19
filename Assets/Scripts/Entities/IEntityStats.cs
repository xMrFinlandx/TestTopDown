namespace Entities
{
    public interface IEntityStats : IDamageable
    {
        public float SpeedModifier { get; }
        public void SetSpeedModifier(float value);
    }
}