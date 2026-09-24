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

    public static LayoutCoordinates LayoutCoordinates(this VisualElement visualElement)
    {
        var resolvedStyle = visualElement.resolvedStyle;
        var worldBound = visualElement.worldBound;
        // var globalMin = element.parent.LocalToWorld(new Vector2(resolvedStyle.top, resolvedStyle.left));
        // var globalMax = element.parent.LocalToWorld(new Vector2(resolvedStyle.bottom, resolvedStyle.right));
        return new LayoutCoordinates(
            // Size:
            Width: worldBound.width,
            Height: worldBound.height,

            // Paddings:
            PaddingTop: resolvedStyle.paddingTop,
            PaddingBottom: resolvedStyle.paddingBottom,
            PaddingLeft: resolvedStyle.paddingLeft,
            PaddingRight: resolvedStyle.paddingRight,

            // Margins:
            MarginTop: resolvedStyle.marginTop,
            MarginBottom: resolvedStyle.marginBottom,
            MarginLeft: resolvedStyle.marginLeft,
            MarginRight: resolvedStyle.marginRight,

            // Local:
            LocalTop: resolvedStyle.top,
            LocalBottom: resolvedStyle.bottom,
            LocalLeft: resolvedStyle.left,
            LocalRight: resolvedStyle.right,

            // Global:
            GlobalTop: worldBound.yMin,
            GlobalBottom: worldBound.yMax,
            GlobalLeft: worldBound.xMin,
            GlobalRight: worldBound.xMax
            // GlobalTop: globalMin.y,
            // GlobalBottom: globalMax.y,
            // GlobalLeft: globalMin.x,
            // GlobalRight: globalMax.x
        );
    }
}