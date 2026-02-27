// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Custom(
        this IModifier modifier,
        Action<VisualElement> apply,
        Action<VisualElement> revert
    )
    {
        return modifier + new CustomModifierImpl(apply, revert);
    }
}

internal class CustomModifierImpl : BaseModifier<CustomModifierImpl>
{
    private readonly Action<VisualElement> _apply;
    private readonly Action<VisualElement> _revert;

    public CustomModifierImpl(Action<VisualElement> apply, Action<VisualElement> revert)
    {
        _apply = apply;
        _revert = revert;
    }

    public override void Apply(VisualElement element) => _apply(element);
    public override void Revert(VisualElement element) => _revert(element);

    protected override bool Equals(CustomModifierImpl other)
    {
        return _apply == other._apply && _revert == other._revert;
    }
}