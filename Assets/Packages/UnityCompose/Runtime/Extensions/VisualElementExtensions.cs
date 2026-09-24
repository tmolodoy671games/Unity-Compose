// ReSharper disable CheckNamespace

using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class VisualElementExtensions
{
    public static IMutableStableDictionary<string, object?> UserData(this VisualElement visualElement)
    {
        if (visualElement.userData is IMutableStableDictionary<string, object?> dictionary)
            return dictionary;
        var newDictionary = MutableStableDictionaryOf<string, object?>();
        visualElement.userData = newDictionary;
        return newDictionary;
    }

    public static VisualElement? GetOrNull(this VisualElement visualElement, int index)
    {
        if (index < 0 || index >= visualElement.childCount)
            return null;
        return visualElement[index];
    }

    public static VisualElement VisualElement(this EventBase evt)
    {
        return (VisualElement)evt.target;
    }
}