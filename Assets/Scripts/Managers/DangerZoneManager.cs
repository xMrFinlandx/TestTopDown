using Gameplay.Zones;
using Scriptables.Zones;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class DangerZoneManager : MonoBehaviour
    {
        [SerializeField] private DangerZone _prefab;
        [SerializeField] private LayerMask _obstacleLayerMask;
        [SerializeField] private DangerZonesConfig _dangerZonesConfig;
        [SerializeField] private BoxCollider2D _bounds;
        [SerializeField] private float _minNeighbourDistance = 3;
        
        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        
        private void Start()
        {
            _minBounds = _bounds.bounds.min;
            _maxBounds = _bounds.bounds.max;
            
            InstantiateZones();
        }

        private void InstantiateZones()
        {
            var uniqueZones = _dangerZonesConfig.DangerZoneData.Count;

            for (int i = 0; i < uniqueZones; i++)
            {
                var zoneData = _dangerZonesConfig.DangerZoneData[i];
                
                for (int j = 0; j < zoneData.Count; j++)
                {
                    if (!OverlapHelper.TryGetRandomPosition(out var position, _minBounds, _maxBounds,
                            zoneData.ZoneConfig.Radius,
                            _minNeighbourDistance, _obstacleLayerMask))
                        continue;
                    
                    var dangerZone = Instantiate(_prefab, position, Quaternion.identity, transform);
                    dangerZone.Init(zoneData.ZoneConfig);
                }
            }
        }
    }
}