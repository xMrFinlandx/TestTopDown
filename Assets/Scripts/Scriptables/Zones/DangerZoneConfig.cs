using Entities;
using UnityEngine;
using Utilities.Enums;

namespace Scriptables.Zones
{
    public abstract class DangerZoneConfig : ScriptableObject
    {
        [SerializeField] private float _radius;
        [SerializeField] private EntityType _targets;
        [SerializeField] private Color _areaColor;

        public float Radius => _radius;
        public EntityType Targets => _targets;
        public Color AreaColor => _areaColor;

        public void OnEnter(IEntityStats target)
        {
            if (Targets.HasFlag(target.EntityType))
                Enter(target);
        }

        public void OnExit(IEntityStats target)
        {
            if (Targets.HasFlag(target.EntityType))
                Exit(target);
        }

        protected abstract void Enter(IEntityStats target);
        
        protected abstract void Exit(IEntityStats target);
    }
}