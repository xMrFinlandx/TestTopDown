using System.Collections;
using Entities;
using Scriptables.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private EnemyStats _prefab;
        [Header("Spawn Settings")]
        [SerializeField] private BoxCollider2D _bounds;
        [SerializeField] private float _initialSpawnInterval = 2f; 
        [SerializeField] private float _spawnDecreaseInterval = 10f; 
        [SerializeField] private float _spawnIntervalDecrease = 0.1f; 
        [SerializeField] private float _minSpawnInterval = 0.5f;
        [Header("Enemy stats")]
        [SerializeField] private EnemyStatsConfig _enemyStatsConfig;

        private float _currentSpawnInterval;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _currentSpawnInterval = _initialSpawnInterval;
            
            StartCoroutine(SpawnEnemies());
            StartCoroutine(DecreaseSpawnInterval());
        }
        
        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(_currentSpawnInterval);
                SpawnEnemy();
            }
        }

        private IEnumerator DecreaseSpawnInterval()
        {
            while (_currentSpawnInterval > _minSpawnInterval)
            {
                yield return new WaitForSeconds(_spawnDecreaseInterval);
                _currentSpawnInterval = Mathf.Max(_currentSpawnInterval - _spawnIntervalDecrease, _minSpawnInterval);
            }
        }

        private void SpawnEnemy()
        {
            var spawnPosition = GetRandomSpawnPosition();
            var enemyStats = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            enemyStats.Init(_enemyStatsConfig);
        }
        
        private Vector2 GetRandomSpawnPosition()
        {
            var bounds = _bounds.bounds;

            Vector2 min = bounds.min;
            Vector2 max = bounds.max;
            Vector2 spawnPosition;

            do
            {
                var randomX = Random.Range(min.x, max.x);
                var randomY = Random.Range(min.y, max.y);
                spawnPosition = new Vector2(randomX, randomY);
            } while (IsInView(spawnPosition));

            return spawnPosition;
        }

        private bool IsInView(Vector2 position)
        {
            var viewportPoint = _camera.WorldToViewportPoint(position);
            
            return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
        }
    }
}