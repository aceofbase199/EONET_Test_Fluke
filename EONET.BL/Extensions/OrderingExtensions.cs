using EONET.BL.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EONET.BL.Extensions
{
    public static class OrderingExtensions
    {
        public static IEnumerable<T> ApplyOrdering<T>(this IEnumerable<T> entities, string propertyName, SortingDirection sortingOrder)
        {
            if (!entities.Any() || string.IsNullOrEmpty(propertyName))
                return entities;

            var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            return sortingOrder == SortingDirection.Ascend
                ? entities.OrderBy(e => propertyInfo.GetValue(e, null))
                : entities.OrderByDescending(e => propertyInfo.GetValue(e, null));
        }
    }
}