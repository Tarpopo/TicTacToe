using System.Collections.Generic;
using System.Linq;
using UnityRandom = UnityEngine.Random;

public static class IEnumerableExtensions
{
    public static T GetRandomElement<T>(this IEnumerable<T> enumerable) =>
        enumerable.ElementAt(UnityRandom.Range(0, enumerable.Count()));
}