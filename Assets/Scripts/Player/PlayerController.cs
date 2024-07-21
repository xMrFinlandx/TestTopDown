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
        
        public bool RotateTowards(Vector2 targetPosition, float deltaTime)
        {
            var direction = (targetPosition - (Vector2) transform.position).normalized;
            var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var currentAngle = _rigidbody.rotation;
            var angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
            var maxRotationStep = _playerStats.RotationSpeed * deltaTime;

            if (Mathf.Abs(angleDifference) <= maxRotationStep)
            {
                _rigidbody.SetRotation(targetAngle);
                return true;
            }

            var rotationStep = Mathf.Sign(angleDifference) * maxRotationStep;
            _rigidbody.SetRotation(currentAngle + rotationStep); 
            return false;
        }
        
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