using Gameplay.Handlers;
using Scriptables.StatusEffects;
using Utilities;

namespace Managers.Spawners
{
    public class StatusEffectContainerSpawner : BaseContainerSpawner<StatusEffectContainer, StatusEffectConfig>
    {
        public override StatusEffectConfig GetItem()
        {
            return Items.GetRandom();
        }
    }
}