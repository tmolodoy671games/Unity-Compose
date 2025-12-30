using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;

internal static class VisualElementExtensions
{
    public static VisualElement? GetOrNull(this VisualElement element, int index)
    {
        return index >= element.childCount ? null : element[index];
    }

    public static string Format(this VisualElement element)
    {
        var result = element.GetType().Name;
        return result;
    }

    public static bool FastReinsert(this VisualElement parent, int index, VisualElement child)
    {
        if (parent.GetOrNull(index) == child) return false;
        child.RemoveFromHierarchy();
        parent.Insert(index, child);
        return true;
    }

    public static void FastRemove(this VisualElement parent, int index, VisualElement child)
    {
        if (parent.GetOrNull(index) == child)
            parent.RemoveAt(index);
        else
            parent.Remove(child);
    }
}