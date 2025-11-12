// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnLocallyPositioned(
        this IModifier modifier,
        Action<OnLocallyPositionedInfo> onGloballyPositioned
    )
    {
        return modifier + new OnLocallyPositionedModifierImpl(onGloballyPositioned);
    }
}

public readonly record struct OnLocallyPositionedInfo(
    Vector2 Size,
    Vector2 Position,
    Vector2 Center,
    Vector2 Min,
    Vector2 Max,
    Vector2 LocalPosition,
    Vector2 LocalCenter,
    Vector2 LocalMin,
    Vector2 LocalMax,
    float PaddingTop,
    float PaddingBottom,
    float PaddingLeft,
    float PaddingRight,
    float MarginTop,
    float MarginBottom,
    float MarginLeft,
    float MarginRight
);

internal class OnLocallyPositionedModifierImpl : BaseModifier<OnLocallyPositionedModifierImpl>
{
    private readonly Action<GeometryChangedEvent> _onGeometryChanged;

    public OnLocallyPositionedModifierImpl(Action<OnLocallyPositionedInfo> onGloballyPositioned)
    {
        _onGeometryChanged = it =>
        {
            var style = it.VisualElement().resolvedStyle;
            var localBound = it.VisualElement().localBound;
            onGloballyPositioned(
                new OnLocallyPositionedInfo(
                    Size: it.newRect.size,
                    Position: it.newRect.position,
                    Center: it.newRect.center,
                    Min: it.newRect.min,
                    Max: it.newRect.max,
                    LocalPosition: localBound.position,
                    LocalMin: localBound.min,
                    LocalMax: localBound.max,
                    LocalCenter: localBound.center,
                    PaddingTop: style.paddingTop,
                    PaddingBottom: style.paddingBottom,
                    PaddingLeft: style.paddingLeft,
                    PaddingRight: style.paddingRight,
                    MarginTop: style.marginTop,
                    MarginBottom: style.marginBottom,
                    MarginLeft: style.marginLeft,
                    MarginRight: style.marginRight
                )
            );
        };
    }

    public override void Apply(VisualElement element)
    {
        element.GetComposeCallback<GeometryChangedEvent>().Add(_onGeometryChanged);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
    }

    public override void Revert(VisualElement element)
    {
        element.GetComposeCallback<GeometryChangedEvent>().Remove(_onGeometryChanged);
    }

    protected override bool Equals(OnLocallyPositionedModifierImpl other)
    {
        return _onGeometryChanged == other._onGeometryChanged;
    }
}