using System;
using Entities;
using UnityEngine;
using Utilities.Enums;

namespace Gameplay.Projectiles
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D), typeof(TrailRenderer))]
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private TrailRenderer _trailRenderer;
        
        public Vector2 Direction;
        
        private bool _canIgnoreCollisions = false;
        private bool _isReleased;
        private Action<Vector2> _destroyAction;
        
        public int Damage { get; protected set; }
        public float Speed { get; protected set; }
        
        protected CircleCollider2D Collider => _collider;
        protected Rigidbody2D Rigidbody => _rigidbody;
        protected TrailRenderer TrailRenderer => _trailRenderer;

        public EntityType Owner { get; private set; } = EntityType.Everything;

        public BaseProjectile SetOwner(EntityType entityType)
        {
            Owner = entityType;
            return this;
        }

        public BaseProjectile SetOnDestroyAction(Action<Vector2> action)
        {
            _destroyAction = action;
            return this;
        }

        public BaseProjectile IgnoreCollisions(bool ignore = true)
        {
            _canIgnoreCollisions = ignore;
            return this;
        }

        public virtual void Initialize(Vector2 from, Vector2 to, float speed, int damage)
        {
            _trailRenderer.Clear();

            _isReleased = false;
            Direction = (to - from).normalized;
            Speed = speed;
            Damage = damage;
        }

        protected virtual void Move()
        {
            _rigidbody.MovePosition(transform.position + Time.fixedDeltaTime * Speed * (Vector3) Direction);
        }

        protected void OnRelease()
        {
            _isReleased = true;
            _destroyAction?.Invoke(transform.position);
            _destroyAction = null;
            _canIgnoreCollisions = false;
        }

        protected abstract void ReleaseProjectile();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_canIgnoreCollisions)
                return;

            if (other.TryGetComponent<IDamageable>(out var damageable) && damageable.EntityType != Owner)
            {
                damageable.TryApplyDamage(Damage);

                if (!_isReleased)
                    ReleaseProjectile();
            }
        }
    }
}