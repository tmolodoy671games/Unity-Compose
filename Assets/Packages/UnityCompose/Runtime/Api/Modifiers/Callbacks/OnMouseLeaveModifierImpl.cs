// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseLeave(
        this IModifier modifier,
        Action<MouseMoveInfo> onMouseLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseLeaveModifierImpl(onMouseLeave);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier OnMouseLeave(
        this IModifier modifier,
        Action onMouseLeave,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + new OnMouseLeaveModifierImpl(_ => onMouseLeave());
    }
}

internal class OnMouseLeaveModifierImpl : BaseModifier<OnMouseLeaveModifierImpl>
{
    private readonly Action<MouseLeaveEvent> _onMouseLeave;

    public OnMouseLeaveModifierImpl(Action<MouseMoveInfo> onMouseEnter)
    {
        _onMouseLeave = it => onMouseEnter(
            new MouseMoveInfo(
                Position: it.mousePosition,
                LocalPosition: it.localMousePosition
            )
        );
    }

    public override void Apply(VisualElement element)
    {
        element.pickingMode = PickingMode.Position;
        element.GetComposeCallback<MouseLeaveEvent>().Add(_onMouseLeave);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
    }

    public override void Revert(VisualElement element)
    {
        element.pickingMode = PickingMode.Ignore;
        element.GetComposeCallback<MouseLeaveEvent>().Remove(_onMouseLeave);
    }

    protected override bool Equals(OnMouseLeaveModifierImpl other)
    {
        return _onMouseLeave == other._onMouseLeave;
    }
}