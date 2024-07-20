using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controls
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameControls.IGameplayActions
    {
        private GameControls _gameControls;
        private Camera _camera;

        public Vector2 MousePosition => GetWorldMousePosition(_gameControls.Gameplay.MousePosition);
        
        public event Action<Vector2> MoveEvent;
        public event Action AttackPerfomedEvent;
        public event Action AttackCancelledEvent;

        public void Disable()
        {
            _gameControls.Gameplay.Disable();
        }

        public void SetGameplay()
        {
            _gameControls.Gameplay.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                AttackPerfomedEvent?.Invoke();

            else if (context.phase == InputActionPhase.Canceled)
                AttackCancelledEvent?.Invoke();
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
        }

        private void OnEnable()
        {
            if (_gameControls != null && _camera != null)
                return;

            _camera = Camera.main;
            _gameControls = new GameControls();
            _gameControls.Gameplay.SetCallbacks(this);
            
            SetGameplay();
        }
        
        private Vector3 GetWorldMousePosition(InputAction context)
        {
            return _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }
    }
}
