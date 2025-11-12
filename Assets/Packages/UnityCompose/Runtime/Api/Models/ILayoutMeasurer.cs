// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface ILayoutMeasurer
{
    Vector2 LocalToGlobal(Vector2 localPosition);
    Vector2 GlobalToLocal(Vector2 globalPosition);
    Vector2 GlobalToScreen(Vector2 globalPosition);
    Vector2 ScreenToGlobal(Vector2 screenPosition);
    Vector2 LocalToScreen(Vector2 localPosition);
    Vector2 ScreenToLocal(Vector2 screenPosition);
}

internal class LayoutMeasurerImpl : ILayoutMeasurer
{
    private readonly VisualElement _visualElement;

    public LayoutMeasurerImpl(VisualElement visualElement)
    {
        _visualElement = visualElement;
    }

    public Vector2 LocalToGlobal(Vector2 localPosition)
    {
        return _visualElement.LocalToWorld(localPosition);
    }

    public Vector2 GlobalToLocal(Vector2 globalPosition)
    {
        return _visualElement.WorldToLocal(globalPosition);
    }

    public Vector2 GlobalToScreen(Vector2 globalPosition)
    {
        var panel = _visualElement.panel.NotNull();
        var rootWorld = panel.visualTree.worldBound;
        var screenPosition = globalPosition + rootWorld.position;
        screenPosition = new Vector2(x: screenPosition.x, y: Screen.height - screenPosition.y);

        return screenPosition;
    }

    public Vector2 ScreenToGlobal(Vector2 screenPosition)
    {
        var newScreenPosition = new Vector2(
            x: screenPosition.x,
            y: Screen.height - screenPosition.y
        );
        var panelPosition = RuntimePanelUtils.ScreenToPanel(_visualElement.panel, newScreenPosition);
        return panelPosition;
    }

    public Vector2 LocalToScreen(Vector2 localPosition)
    {
        return GlobalToScreen(LocalToGlobal(localPosition));
    }

    public Vector2 ScreenToLocal(Vector2 screenPosition)
    {
        return GlobalToLocal(ScreenToGlobal(screenPosition));
    }
}