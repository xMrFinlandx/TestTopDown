using System.Collections.Generic;
using UnityEngine;
using Utilities.Structures;

namespace Gameplay.Zones
{
    [CreateAssetMenu(fileName = "New Danger Zones Config", menuName = "Zones/Danger Zones Config", order = 0)]
    public class DangerZonesConfig : ScriptableObject
    {
        [SerializeField] private DangerZoneData[] _dangerZoneData;

        public IReadOnlyList<DangerZoneData> DangerZoneData => _dangerZoneData;
    }
}