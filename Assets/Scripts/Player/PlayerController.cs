using Player.Controls;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [Space]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private PlayerStats _playerStats;
        
        private Vector2 _moveDirection;
        
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            _inputReader.MoveEvent += OnMove;
        }

        private void FixedUpdate()
        {
            var targetVelocity = _moveDirection * _playerStats.Speed;
            
            if (_moveDirection != Vector2.zero)
            {
                _rigidbody.velocity = Vector2.MoveTowards(_rigidbody.velocity, targetVelocity, _playerStats.Speed);
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }

        private void OnMove(Vector2 direction)
        {
            _moveDirection = direction;
        }

        private void OnDisable()
        {
            _inputReader.MoveEvent -= OnMove;
        }
    }
}