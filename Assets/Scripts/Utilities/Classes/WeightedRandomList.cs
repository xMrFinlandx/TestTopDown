using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utilities.Classes
{
    [Serializable]
    public class WeightedRandomList<T>
    {
        [Serializable]
        public struct Pair
        {
            public T Item;
            public float Weight;

            public Pair(T item, float weight)
            {
                Item = item;
                Weight = weight;
            }
        }

        public List<Pair> WeightedList = new();

        public int Count => WeightedList.Count;

        public void Add(T item, float weight)
        {
            WeightedList.Add(new Pair(item, weight));
        }

        public T Get()
        {
            var totalWeight = WeightedList.Sum(p => p.Weight);
            var value = Random.value * totalWeight;
            float sumWeight = 0;

            foreach (var pair in WeightedList)
            {
                sumWeight += pair.Weight;

                if (sumWeight >= value)
                {
                    return pair.Item;
                }
            }
            return default;
        }
    }
}