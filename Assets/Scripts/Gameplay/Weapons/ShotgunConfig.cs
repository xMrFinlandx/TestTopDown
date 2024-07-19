using Managers;
using UnityEngine;
using Utilities;

namespace Gameplay.Weapons
{
    [CreateAssetMenu(fileName = "New Shotgun Data", menuName = "Weapons/Shotgun Data", order = 0)]
    public class ShotgunConfig : WeaponConfig
    {
        [SerializeField] private int _projectileCount = 5; 
        [SerializeField] private float _spreadAngle = 10f;
        [SerializeField] private float _maxDistance = 7f; 
        
        public override void Attack(Vector2 from, Vector2 to)
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                ProjectileManager.Instance.GetDistanceLimitedBullet(from, to, ProjectileSpeed, Damage).AddSpread(_spreadAngle).SetMaxDistance(_maxDistance);
            }
        }
    }
}