// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class VisualElementExtensions
{
    public static IMutableStableDictionary<object?, object?> UserData(this VisualElement visualElement)
    {
        if (visualElement.userData is IMutableStableDictionary<object?, object?> dictionary)
            return dictionary;
        var newDictionary = MutableStableDictionaryOf<object?, object?>();
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

    public static ILayoutCoordinates LayoutCoordinates(this VisualElement visualElement)
    {
        return new LayoutCoordinatesImpl(visualElement);
    }
}