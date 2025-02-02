﻿using UnityEngine;
using Utilities;

namespace Player
{
    public class CameraFollow : Singleton<CameraFollow>
    {
        [SerializeField] private BoxCollider2D _bounds;
        [SerializeField] private float _smoothTime;
        
        private float _cameraHalfWidth;
        private float _cameraHalfHeight;
        
        private Transform _target;
        
        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        
        private Vector3 _velocity;

        private void Start()
        {   
            var mainCamera = Camera.main;
            _cameraHalfHeight = mainCamera.orthographicSize;
            _cameraHalfWidth = mainCamera.aspect * _cameraHalfHeight;

            var bounds = _bounds.bounds;
            _minBounds = bounds.min;
            _maxBounds = bounds.max;
        }

        public void InitTarget(Transform target) => _target = target;
        
        private void LateUpdate()
        {
            if (_target == null)
                return;
            
            var targetPosition = _target.position;

            targetPosition.x = Mathf.Clamp(targetPosition.x, _minBounds.x + _cameraHalfWidth, _maxBounds.x - _cameraHalfWidth);
            targetPosition.y = Mathf.Clamp(targetPosition.y, _minBounds.y + _cameraHalfHeight, _maxBounds.y - _cameraHalfHeight);
            
            var position = transform.position;
            targetPosition.z = position.z;
            
            position = Vector3.SmoothDamp(position, targetPosition, ref _velocity, _smoothTime);
            transform.position = position;
        }
    }
}