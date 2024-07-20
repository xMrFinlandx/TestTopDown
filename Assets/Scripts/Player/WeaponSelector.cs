using Player.Controls;
using Scriptables.Weapons;
using UnityEngine;

namespace Player
{
    public class WeaponSelector : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private WeaponConfig _weaponConfig;

        private void Start()
        {
            _inputReader.AttackPerfomedEvent += OnAttack;
        }

        private void OnAttack(Vector2 targetPosition)
        {
            _weaponConfig.Attack(transform.position, targetPosition);
        }

        private void OnDisable()
        {
            _inputReader.AttackPerfomedEvent -= OnAttack;
        }
    }
}