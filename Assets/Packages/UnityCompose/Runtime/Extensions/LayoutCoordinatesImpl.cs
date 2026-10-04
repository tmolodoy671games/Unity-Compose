// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal sealed class LayoutCoordinatesImpl : ILayoutCoordinates
{
    private readonly VisualElement _element;

    public LayoutCoordinatesImpl(VisualElement element)
    {
        _element = element;
    }

    public Size Size
    {
        get
        {
            var rect = _element.layout;

            return new Size(
                rect.width,
                rect.height);
        }
    }

    public ILayoutCoordinates? ParentCoordinates =>
        _element.parent?.LayoutCoordinates();

    public Offset LocalPositionOf(
        ILayoutCoordinates sourceCoordinates,
        Offset relativeToSource
    )
    {
        var sourceElement = GetElement(sourceCoordinates);

        var point = sourceElement.ChangeCoordinatesTo(
            _element,
            relativeToSource.ToVector2()
        );

        return point.ToOffset();
    }

    public Offset ScreenToLocal(
        Offset relativeToScreen
    )
    {
        return default; // BRUH
    }

    public Offset LocalToScreen(
        Offset relativeToLocal
    )
    {
        return default; // BRUH
    }

    public Offset RootToLocal(
        Offset relativeToRoot
    )
    {
        var root = GetRoot();
        var relativeToRootVector = relativeToRoot.ToVector2();
        relativeToRootVector = root.LocalToWorld(relativeToRootVector);
        relativeToRootVector = _element.WorldToLocal(relativeToRootVector);
        return relativeToRootVector.ToOffset();
    }

    public Offset LocalToRoot(
        Offset relativeToLocal
    )
    {
        var root = GetRoot();
        var relativeToLocalVector = relativeToLocal.ToVector2();
        relativeToLocalVector = _element.LocalToWorld(relativeToLocalVector);
        relativeToLocalVector = root.WorldToLocal(relativeToLocalVector);
        return relativeToLocalVector.ToOffset();
    }

    private static VisualElement GetElement(
        ILayoutCoordinates coordinates
    )
    {
        return coordinates switch
        {
            LayoutCoordinatesImpl impl => impl._element,

            _ => throw new ArgumentException(
                "The provided LayoutCoordinates implementation " +
                "does not belong to UnityCompose.",
                nameof(coordinates))
        };
    }

    private VisualElement GetRoot()
    {
        return _element.panel?.visualTree ?? FindRoot();
    }

    private VisualElement FindRoot()
    {
        var current = _element;
        while (current.parent != null)
            current = current.parent;
        return current;
    }
}