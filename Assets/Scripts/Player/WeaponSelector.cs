using System;
using Player.Controls;
using Scriptables.Weapons;
using UnityEngine;
using Utilities.Classes;

namespace Player
{
    public class WeaponSelector : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _firePoint;
        
        private bool _isAttackPressed;
        
        private WeaponHolder _weaponHolder;

        public static event Action<WeaponConfig> WeaponChangedAction; 
        
        public WeaponConfig CurrentWeapon => _weaponHolder.WeaponConfig;
        
        public void Set(WeaponConfig item)
        {
            WeaponChangedAction?.Invoke(item);
            _weaponHolder = new WeaponHolder(item);
        }
        
        private void Start()
        {
            _inputReader.AttackPerfomedEvent += OnAttackPerfomed;
            _inputReader.AttackCancelledEvent += OnAttackCancelled;
        }

        private void Update()
        {
            _weaponHolder.Update(Time.deltaTime);

            if (_isAttackPressed && _weaponHolder.CanAttack && _playerController.RotateTowards(_inputReader.MousePosition, Time.deltaTime))
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