// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseMove(
        this IModifier modifier,
        Action<MouseMoveInfo> onMouseMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseMoveModifierImpl(onMouseMove);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseMove(
        this IModifier modifier,
        Action onMouseMove,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseMoveModifierImpl(_ => onMouseMove());
    }
}

internal class OnMouseMoveModifierImpl : BaseModifier<OnMouseMoveModifierImpl>
{
    private readonly Action<MouseMoveEvent> _onMouseMove;

    public OnMouseMoveModifierImpl(Action<MouseMoveInfo> onMouseMove)
    {
        _onMouseMove = it => onMouseMove(
            new MouseMoveInfo(
                Position: it.mousePosition,
                LocalPosition: it.localMousePosition
            )
        );
    }

    public override void Apply(VisualElement element)
    {
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<MouseMoveEvent>().Add(_onMouseMove);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
    }

    public override void Revert(VisualElement element)
    {
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseMoveEvent>().Add(_onMouseMove);
    }

    protected override bool Equals(OnMouseMoveModifierImpl other)
    {
        return _onMouseMove == other._onMouseMove;
    }
}