using Player.Controls;
using Scriptables.Weapons;
using UnityEngine;
using Utilities.Classes;

namespace Player
{
    public class WeaponSelector : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _firePoint;

        private WeaponHolder _weaponHolder;
        
        private bool _isAttackPressed;
        
        private void Start()
        {
            _inputReader.AttackPerfomedEvent += OnAttackPerfomed;
            _inputReader.AttackCancelledEvent += OnAttackCancelled;
            _weaponHolder = new WeaponHolder(_weaponConfig);
        }

        private void Update()
        {
            _weaponHolder.Update(Time.deltaTime);

            if (_isAttackPressed && _weaponHolder.CanAttack && _playerController.RotateTowards(_inputReader.MousePosition))
            {
                _weaponHolder.Attack(_firePoint.position, _inputReader.MousePosition);
            }
        }

        private void OnAttackPerfomed()
        {
            _isAttackPressed = true;
        }
        
        private void OnAttackCancelled()
        {
            _isAttackPressed = false;
        }

        private void OnDisable()
        {
            _inputReader.AttackPerfomedEvent -= OnAttackPerfomed;
            _inputReader.AttackCancelledEvent -= OnAttackCancelled;
        }
    }
}