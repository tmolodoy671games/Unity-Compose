using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;

internal static class StyleExtensions
{
    public static void AddFilter(this IStyle style, FilterFunction filter)
    {
        if (style.filter.value == null)
            style.filter = new List<FilterFunction>();
        var newFilters = new List<FilterFunction>(style.filter.value) { filter };
        style.filter = newFilters;
    }

    public static void RemoveFilter(this IStyle style, FilterFunction filter)
    {
        if (style.filter.value == null)
            style.filter = new List<FilterFunction>();
        var newFilters = new List<FilterFunction>(style.filter.value);
        newFilters.Remove(filter);
        style.filter = newFilters;
    }
    
    public static void AddBackdropFilter(this IStyle style, FilterFunction filter)
    {
        if (style.backdropFilter.value == null)
            style.backdropFilter = new List<FilterFunction>();
        var newFilters = new List<FilterFunction>(style.backdropFilter.value) { filter };
        style.backdropFilter = newFilters;
    }

    public static void RemoveBackdropFilter(this IStyle style, FilterFunction filter)
    {
        if (style.backdropFilter.value == null)
            style.backdropFilter = new List<FilterFunction>();
        var newFilters = new List<FilterFunction>(style.backdropFilter.value);
        newFilters.Remove(filter);
        style.backdropFilter = newFilters;
    }
}