using System.Collections.Generic;
using System.Linq;

namespace Patterns.Contract
{
    public static class CollectionExtensions
    {
        public static bool IsEquivalentIgnoringOrderTo<T>(this IReadOnlyCollection<T> source, IReadOnlyCollection<T> target)
        {
            return source.Except(target).Any() == false && source.Count == target.Count;
        }
    }
}