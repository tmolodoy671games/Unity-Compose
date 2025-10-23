// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnGloballyPositioned(
        this IModifier modifier,
        Action<OnGloballyPositionedInfo> onGloballyPositioned
    )
    {
        return modifier + new OnGloballyPositionedModifierImpl(onGloballyPositioned);
    }
}

public readonly record struct OnGloballyPositionedInfo(
    Vector2 Position,
    Vector2 Size,
    Vector2 Center,
    Vector2 Min,
    Vector2 Max,
    float PaddingTop,
    float PaddingBottom,
    float PaddingLeft,
    float PaddingRight,
    float MarginTop,
    float MarginBottom,
    float MarginLeft,
    float MarginRight
);

internal class OnGloballyPositionedModifierImpl : BaseModifier<OnGloballyPositionedModifierImpl>
{
    private readonly Action<GeometryChangedEvent> _onGeometryChanged;

    public OnGloballyPositionedModifierImpl(Action<OnGloballyPositionedInfo> onGloballyPositioned)
    {
        _onGeometryChanged = it =>
        {
            var style = it.VisualElement().resolvedStyle;
            onGloballyPositioned(
                new OnGloballyPositionedInfo(
                    Position: it.newRect.position,
                    Size: it.newRect.size,
                    Center: it.newRect.center,
                    Min: it.newRect.min,
                    Max: it.newRect.max,
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

    protected override bool Equals(OnGloballyPositionedModifierImpl other)
    {
        return _onGeometryChanged == other._onGeometryChanged;
    }
}