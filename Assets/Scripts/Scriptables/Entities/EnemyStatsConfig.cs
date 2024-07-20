using UnityEngine;

namespace Scriptables.Entities
{
    [CreateAssetMenu(fileName = "New Enemy Stats Config", menuName = "Entities/Enemy Stats Config", order = 0)]
    public class EnemyStatsConfig : EntityStatsConfig
    {
        [SerializeField] private Color _color;
        [SerializeField] private int _points;

        public Color Color => _color;
        public int Points => _points;
    }
}