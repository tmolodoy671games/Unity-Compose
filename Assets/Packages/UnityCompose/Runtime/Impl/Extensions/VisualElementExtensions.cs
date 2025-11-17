using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

internal static class VisualElementExtensions
{
    public static VisualElement? GetOrNull(this VisualElement element, int index)
    {
        return index >= element.childCount ? null : element[index];
    }

    public static string Format(this VisualElement element)
    {
        var result = element.GetType().Name;
        if (element.name is { Length: > 0 })
            result += $"({element.name})";
        if (element.parent != null)
            result += $"[{element.parent.IndexOf(element)}]";
        if (element is Label label)
            result += $".text={label.text}";
        return result;
    }

    public static bool FastReinsert(this VisualElement parent, int index, VisualElement child)
    {
        if (parent.GetOrNull(index) == child) return false;
        child.RemoveFromHierarchy();
        parent.Insert(index, child);
        return true;
    }
}