using Player;
using Scriptables.StatusEffects;

namespace Utilities.Classes
{
    public class StatusEffectHolder
    {
        private readonly StatusEffectConfig _statusEffectConfig;
        private readonly PlayerStats _target;
        private float _lifeTime;

        public bool CanRemove => _lifeTime < 0;
        
        public StatusEffectHolder(StatusEffectConfig statusEffectConfig, PlayerStats target)
        {
            _statusEffectConfig = statusEffectConfig;
            _lifeTime = statusEffectConfig.LifeTime;
            _target = target;
        }

        public void Apply()
        {
            _statusEffectConfig.OnApply(_target);
        }

        public void OnRemove()
        {
            _statusEffectConfig.OnRemove(_target);
        }

        public void Update(float deltaTime)
        {
            _lifeTime -= deltaTime;
        }
    }
}