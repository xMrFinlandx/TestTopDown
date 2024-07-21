using System.Collections;
using System.Collections.Generic;
using Entities;
using Managers.Queue;
using Player;
using Scriptables.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawner : QueueElement
    {
        [SerializeField] private EnemyStats _prefab;
        [Header("Spawn Settings")]
        [SerializeField] private BoxCollider2D _bounds;
        [SerializeField] private float _initialSpawnInterval = 2f; 
        [SerializeField] private float _spawnDecreaseInterval = 10f; 
        [SerializeField] private float _spawnIntervalDecrease = 0.1f; 
        [SerializeField] private float _minSpawnInterval = 0.5f;
        [Header("Enemy stats")]
        [SerializeField] private EnemySpawnConfig _spawnConfig;

        private List<EnemyStats> _enemies = new();
        
        private float _currentSpawnInterval;
        private Camera _camera;
        
        public override void Enable()
        {
            KillEnemies();

            _currentSpawnInterval = _initialSpawnInterval;
            
            StartCoroutine(SpawnEnemies());
            StartCoroutine(DecreaseSpawnInterval());
        }

        private void Start()
        {
            _camera = Camera.main;
            EnemyStats.EnemyDiedAction += OnEnemyDied;
            PlayerStats.PlayerDiedAction += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            StopAllCoroutines();
        }

        private void OnEnemyDied(EnemyStats enemy)
        {
            _enemies.Remove(enemy);
        }

        private void KillEnemies()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }

            _enemies.Clear();
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
            enemyStats.Init(_spawnConfig.Get(), PlayerSpawner.PlayerTransform);
            _enemies.Add(enemyStats);
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

        private void OnDestroy()
        {
            EnemyStats.EnemyDiedAction -= OnEnemyDied;
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
        }
    }
}