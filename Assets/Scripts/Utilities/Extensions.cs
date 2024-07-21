using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Extensions
    {
        public static T GetRandom<T>(this IReadOnlyList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}