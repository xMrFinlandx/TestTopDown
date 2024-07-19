using Entities;
using UnityEngine;

namespace Gameplay.Zones
{
    public abstract class DangerZoneConfig : ScriptableObject
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Color _areaColor;

        public float Radius => _radius;
        public LayerMask LayerMask => _layerMask;
        public Color AreaColor => _areaColor;

        public abstract void OnEnter(IEntityStats target);
        
        public abstract void OnExit(IEntityStats target);
    }
}