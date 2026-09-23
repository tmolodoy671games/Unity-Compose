// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class CustomModifierImpl : BaseModifier<CustomModifierImpl>
{
    private readonly Action<IReusableComposeNode> _apply;
    private readonly Action<IReusableComposeNode> _revert;

    public CustomModifierImpl(Action<IReusableComposeNode> apply, Action<IReusableComposeNode> revert)
    {
        _apply = apply;
        _revert = revert;
    }

    public override void Apply(IReusableComposeNode node)
    {
        _apply(node);
    }

    public override void Revert(IReusableComposeNode node)
    {
        _revert(node);
    }

    protected override bool Equals(CustomModifierImpl other)
    {
        return _apply == other._apply && _revert == other._revert;
    }
}