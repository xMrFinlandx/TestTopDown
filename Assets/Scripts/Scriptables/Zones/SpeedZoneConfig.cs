using Entities;
using UnityEngine;

namespace Scriptables.Zones
{
    [CreateAssetMenu(fileName = "New Speed Zone", menuName = "Zones/Speed Zone", order = 0)]
    public class SpeedZoneConfig : DangerZoneConfig
    {
        [SerializeField] private float _speedModifier = .6f;
        
        protected override void Enter(IEntityStats target)
        {
            target.SetSpeedModifier(_speedModifier);
        }

        protected override void Exit(IEntityStats target)
        {
            target.SetSpeedModifier(1);
        }
    }
}