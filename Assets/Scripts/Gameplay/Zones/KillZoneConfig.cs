using Entities;
using UnityEngine;

namespace Gameplay.Zones
{
    [CreateAssetMenu(fileName = "New Kill Zone", menuName = "Zones/Kill Zone", order = 0)]
    public class KillZoneConfig : DangerZoneConfig
    {
        public override void OnEnter(IEntityStats target)
        {
            target.Kill();
        }

        public override void OnExit(IEntityStats target)
        {
        }
    }
}