using System;
using Player;
using UnityEngine;

namespace Gameplay.Handlers
{
    public abstract class BaseContainer<T> : MonoBehaviour, ICollectable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public static event Action<BaseContainer<T>> ContainerDestroyedAction;
        
        private float _lifeTimeCounter;

        protected T Item { get; private set; }
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public void Init(T item, float lifeTime)
        {
            Item = item;
            _lifeTimeCounter = lifeTime;
            
            OnInit();
        }

        public void Destroy()
        {
            ContainerDestroyedAction?.Invoke(this);
            Destroy(gameObject);
        }

        public abstract void OnInit();

        public abstract void Collect(PlayerStats target);

        private void Update()
        {
            _lifeTimeCounter -= Time.deltaTime;
            
            if (_lifeTimeCounter > 0)
                return;

            Destroy();
        }
    }
}