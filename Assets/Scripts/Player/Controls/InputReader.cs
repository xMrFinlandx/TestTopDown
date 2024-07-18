using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controls
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameControls.IGameplayActions
    {
        private GameControls _gameControls;
        
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

        private void OnEnable()
        {
            if (_gameControls != null)
                return;

            _gameControls = new GameControls();
            _gameControls.Gameplay.SetCallbacks(this);
            
            SetGameplay();
        }
    }
}
