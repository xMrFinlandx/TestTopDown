using Gameplay.Handlers;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private PlayerStats _playerStats;

        private void FixedUpdate()
        {
            var results = OverlapHelper.GetComponentsInCircle<ICollectable>(transform.position, _range, _layerMask);

            foreach (var result in results)
            {
                result.Collect(_playerStats);
            }
        }
    }
}