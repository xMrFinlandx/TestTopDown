using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class OverlapHelper
    {
        public static List<T> GetComponentsInCircle<T>(Vector2 center, float range, LayerMask layerMask)
        {
            var results = new List<T>();
            var colliders = Physics2D.OverlapCircleAll(center, range, layerMask);

            foreach (var item in colliders)
            {
                if (item.TryGetComponent<T>(out var target))
                    results.Add(target);
            }

            return results;
        }

        public static bool TryGetRandomPosition(out Vector2 result, Vector2 minBounds, Vector2 maxBounds, float range, float minNeighbourDistance, LayerMask layerMask, int attempts = 50)
        {
            result = Vector2.zero;
            
            for (int i = 0; i < attempts; i++)
            {
                result.x = Random.Range(minBounds.x + range + minNeighbourDistance, maxBounds.x - range - minNeighbourDistance);
                result.y = Random.Range(minBounds.y + range + minNeighbourDistance, maxBounds.y - range - minNeighbourDistance);

                if (!IsAreaContainsCollision(result, range + minNeighbourDistance, layerMask))
                {
                    return true;
                }
            }
            
            return false;
        }

        public static bool IsAreaContainsCollision(Vector2 center, float range, LayerMask layerMask)
        {
            return Physics2D.OverlapCircle(center, range, layerMask);
        }
    }
}