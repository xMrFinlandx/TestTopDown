using UnityEngine;

namespace Scriptables.Entities
{
    [CreateAssetMenu(fileName = "New Player Stats Config", menuName = "Entities/Player Stats Config", order = 0)]
    public class PlayerStatsConfig : EntityStatsConfig
    {
        [SerializeField] private float _rotationSpeed = 180;

        public float RotationSpeed => _rotationSpeed;
    }
}