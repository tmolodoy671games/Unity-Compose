// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class CustomModifierImpl : UnityModifier<CustomModifierImpl>
{
    private readonly Action<IReusableComposeNode> _apply;
    private readonly Action<IReusableComposeNode> _revert;

    public CustomModifierImpl(Action<IReusableComposeNode> apply, Action<IReusableComposeNode> revert)
    {
        _apply = apply;
        _revert = revert;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        _apply(node);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        _revert(node);
    }

    protected override bool Equals(CustomModifierImpl other) => _apply == other._apply && _revert == other._revert;
    public override int GetHashCode() => HashCode.Combine(_apply, _revert);
}