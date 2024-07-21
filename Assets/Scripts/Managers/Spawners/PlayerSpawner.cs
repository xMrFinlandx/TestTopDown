using Managers.Queue;
using Player;
using UnityEngine;

namespace Managers.Spawners
{
    public class PlayerSpawner : QueueElement
    {
        [SerializeField] private PlayerStats _playerPrefab;

        public static Transform PlayerTransform { get; private set; }
        public static PlayerStats PlayerStats { get; private set; }

        public override void Enable()
        {
            PlayerStats = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity);
            
            print(PlayerStats == null);
            
            PlayerTransform = PlayerStats.transform;
            CameraFollow.Instance.InitTarget(PlayerTransform);
        }
    }
}