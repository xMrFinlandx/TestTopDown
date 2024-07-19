using System;
using Gameplay.Zones;
using UnityEngine;

namespace Utilities.Structures
{
    [Serializable]
    public struct DangerZoneData
    {
        [SerializeField] private int _count;
        [SerializeField] private DangerZoneConfig _zoneConfig;

        public int Count => _count;
        public DangerZoneConfig ZoneConfig => _zoneConfig;
    }
}