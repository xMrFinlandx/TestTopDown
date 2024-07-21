using Managers;
using Managers.Spawners;
using Player;
using UnityEngine;

namespace Entities
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemyStats _enemyStats;
        [SerializeField] private float _directionUpdateInterval = .5f;

        private bool _isPlayerAlive = true;
        private float _directionUpdateIntervalCounter;
        private Transform _target;
        private Vector2 _moveDirection;

        private void Start()
        {
            _target = PlayerSpawner.PlayerTransform;
            PlayerStats.PlayerDiedAction += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _isPlayerAlive = false;
        }

        private void FixedUpdate()
        {
            if (!_isPlayerAlive)
                return;
            
            _rigidbody.MovePosition((Vector2) transform.position + _moveDirection * (Time.fixedDeltaTime * _enemyStats.Speed));

            _directionUpdateIntervalCounter -= Time.fixedDeltaTime;
            
            if (_directionUpdateIntervalCounter > 0)
                return;

            _directionUpdateIntervalCounter = _directionUpdateInterval;
            UpdateDirection();
        }

        private void UpdateDirection()
        {
            _moveDirection = (_target.position - transform.position).normalized;
        }

        private void OnDestroy()
        {
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
        }
    }
}