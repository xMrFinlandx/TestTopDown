using Player;
using UnityEngine;

namespace Managers
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerPrefab;

        public static Transform PlayerTransform { get; private set; }

        private void Start()
        {
            PlayerTransform = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity).transform;
            CameraFollow.Instance.InitTarget(PlayerTransform);
        }
    }
}