using Entities;
using UnityEngine;

namespace Gameplay.Zones
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DangerZone : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private SpriteRenderer _areaSpriteRenderer;
        
        private DangerZoneConfig _dangerZoneConfig;
        
        public void Init(DangerZoneConfig config)
        {
            _dangerZoneConfig = config;
            _collider.radius = config.Radius;

            _areaSpriteRenderer.color = config.AreaColor;
            _areaSpriteRenderer.transform.localScale = new Vector3(config.Radius, config.Radius) * 2;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IEntityStats>(out var entityStats))
                _dangerZoneConfig.OnEnter(entityStats);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<IEntityStats>(out var entityStats))
                _dangerZoneConfig.OnExit(entityStats);
        }
    }
}