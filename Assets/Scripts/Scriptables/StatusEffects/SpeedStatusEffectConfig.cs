using Player;
using UnityEngine;

namespace Scriptables.StatusEffects
{
    [CreateAssetMenu(fileName = "New Speed Status Effect Config", menuName = "Status Effects/Speed", order = 0)]
    public class SpeedStatusEffectConfig : StatusEffectConfig
    {
        [SerializeField] private float _speedModifier = 1.5f;
        
        public override void OnApply(PlayerStats playerStats)
        {
            playerStats.SetStatusEffectSpeedModifier(_speedModifier);
        }

        public override void OnRemove(PlayerStats playerStats)
        {
            playerStats.SetStatusEffectSpeedModifier(1);
        }
    }
}