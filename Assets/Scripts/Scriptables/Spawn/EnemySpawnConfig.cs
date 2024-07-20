using Scriptables.Entities;
using UnityEngine;
using Utilities.Classes;

namespace Scriptables.Spawn
{
    [CreateAssetMenu(fileName = "New Enemy Spawn Config", menuName = "Entities/Spawn/Enemy Spawn Config", order = 0)]
    public class EnemySpawnConfig : ScriptableObject
    {
        [SerializeField] private WeightedRandomList<EnemyStatsConfig> _enemies;

        public EnemyStatsConfig Get() => _enemies.Get();
    }
}