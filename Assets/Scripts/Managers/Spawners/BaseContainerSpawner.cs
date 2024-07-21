using System.Collections.Generic;
using Gameplay.Handlers;
using Managers.Queue;
using Player;
using UnityEngine;

namespace Managers.Spawners
{
    public abstract class BaseContainerSpawner<TContainer, SItem> : QueueElement where TContainer : BaseContainer<SItem> where SItem : ScriptableObject
    {
        [SerializeField] private float _containerLifeTime;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private SItem[] _items;
        [SerializeField] private TContainer _prefab;

        private bool _isPaused;
        private Camera _camera;

        protected IReadOnlyList<SItem> Items => _items;
        
        private static readonly List<TContainer> _containers = new();
        
        private float _spawnIntervalCounter;

        public override void Enable()
        {
            _spawnIntervalCounter = _spawnInterval;
            _isPaused = false;

            DestroyContainers();
        }

        public abstract SItem GetItem();

        private static void DestroyContainers()
        {
            foreach (var container in _containers)
            {
                Destroy(container.gameObject);
            }

            _containers.Clear();
        }

        private void Start()
        {
            _camera = Camera.main;
            BaseContainer<SItem>.ContainerDestroyedAction += OnContainerDestroyed;
            PlayerStats.PlayerDiedAction += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _isPaused = true;
        }

        private void OnDestroy()
        {
            BaseContainer<SItem>.ContainerDestroyedAction -= OnContainerDestroyed;
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
        }

        private void OnContainerDestroyed(BaseContainer<SItem> obj)
        {
            if (obj is TContainer container)
                _containers.Remove(container);
        }

        private void Update()
        {
            if (_isPaused)
                return;
            
            _spawnIntervalCounter -= Time.deltaTime;
            
            if (_spawnIntervalCounter > 0)
                return;

            _spawnIntervalCounter = _spawnInterval;
            CreateContainer();
        }

        private void CreateContainer()
        {
            var container = Instantiate(_prefab, GetRandomPositionInView(), Quaternion.identity);
            container.Init(GetItem(), _containerLifeTime);
            _containers.Add(container);
        }
        
        private Vector2 GetRandomPositionInView()
        {
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
            var topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
            
            var randomX = Random.Range(bottomLeft.x, topRight.x);
            var randomY = Random.Range(bottomLeft.y, topRight.y);

            return new Vector2(randomX, randomY);
        }
    }
}