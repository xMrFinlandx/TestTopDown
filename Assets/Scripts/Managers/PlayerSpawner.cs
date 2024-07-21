using Managers.Queue;
using Player;
using UnityEngine;

namespace Managers
{
    public class PlayerSpawner : QueueElement
    {
        [SerializeField] private PlayerStats _playerPrefab;

        public static Transform PlayerTransform { get; private set; }

        public override void Enable()
        {
            PlayerTransform = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity).transform;
            CameraFollow.Instance.InitTarget(PlayerTransform);
        }
    }
}