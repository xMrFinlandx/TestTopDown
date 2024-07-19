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

        public static Vector2 GetRandomPosition(Vector2 minBounds, Vector2 maxBounds, float range, LayerMask layerMask, int attempts = 50)
        {
            var position = Vector2.zero;
            
            for (int i = 0; i < attempts; i++)
            {
                position.x = Random.Range(minBounds.x + range, maxBounds.x - range);
                position.y = Random.Range(minBounds.y + range, maxBounds.y - range);

                if (!IsAreaContainsCollision(position, range, layerMask))
                {
                    return position;
                }
            }
            
            return Vector2.zero;
        }

        public static bool IsAreaContainsCollision(Vector2 center, float range, LayerMask layerMask)
        {
            return Physics2D.OverlapCircle(center, range, layerMask);
        }
    }
}