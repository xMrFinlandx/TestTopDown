using Entities;
using UnityEngine;

namespace Gameplay.Zones
{
    [CreateAssetMenu(fileName = "New Speed Zone", menuName = "Zones/Speed Zone", order = 0)]
    public class SpeedZoneConfig : DangerZoneConfig
    {
        [SerializeField] private float _speedModifier = .6f;
        
        public override void OnEnter(IEntityStats target)
        {
            target.SetSpeedModifier(_speedModifier);
        }

        public override void OnExit(IEntityStats target)
        {
            target.SetSpeedModifier(1);
        }
    }
}