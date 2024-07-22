using Entities;
using UnityEngine;

namespace Scriptables.Zones
{
    [CreateAssetMenu(fileName = "New Kill Zone", menuName = "Zones/Kill Zone", order = 0)]
    public class KillZoneConfig : DangerZoneConfig
    {
        protected override void Enter(IEntityStats target)
        {
            if (Targets.HasFlag(target.EntityType))
                target.Kill();
        }

        protected override void Exit(IEntityStats target)
        {
        }
    }
}