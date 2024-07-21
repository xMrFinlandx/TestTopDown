using Player;
using UnityEngine;

namespace Scriptables.StatusEffects
{
    [CreateAssetMenu(fileName = "New Invisibility Status Effect Config", menuName = "Status Effects/Invisibility", order = 0)]
    public class InvisibilityStatusEffectConfig : StatusEffectConfig
    {
        public override void OnApply(PlayerStats playerStats)
        {
            playerStats.SetInvisibility(LifeTime);
        }

        public override void OnRemove(PlayerStats playerStats)
        {
        }
    }
}