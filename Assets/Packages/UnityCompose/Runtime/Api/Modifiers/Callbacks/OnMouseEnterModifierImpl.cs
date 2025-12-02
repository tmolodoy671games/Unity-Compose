// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnMouseEnter(
        this IModifier modifier,
        Action<MouseMoveInfo> onMouseEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseEnterModifierImpl(onMouseEnter);
    }

    public static IModifier OnMouseEnter(
        this IModifier modifier,
        Action onMouseEnter,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseEnterModifierImpl(_ => onMouseEnter());
    }
}

public readonly record struct MouseMoveInfo(
    Vector2 Position,
    Vector2 LocalPosition
);

internal class OnMouseEnterModifierImpl : BaseModifier<OnMouseEnterModifierImpl>
{
    private readonly Action<MouseEnterEvent> _onMouseEnter;

    public OnMouseEnterModifierImpl(Action<MouseMoveInfo> onMouseEnter)
    {
        _onMouseEnter = it => onMouseEnter(
            new MouseMoveInfo(
                Position: it.mousePosition,
                LocalPosition: it.localMousePosition
            )
        );
    }

    public override void Apply(VisualElement element)
    {
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<MouseEnterEvent>().Add(_onMouseEnter);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
    }

    public override void Revert(VisualElement element)
    {
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseEnterEvent>().Remove(_onMouseEnter);
    }

    protected override bool Equals(OnMouseEnterModifierImpl other)
    {
        return _onMouseEnter == other._onMouseEnter;
    }
}