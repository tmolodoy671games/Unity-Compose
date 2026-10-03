// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class CapturePointerModifierImpl : UnityModifier<CapturePointerModifierImpl>
{
    private readonly int _pointerId;

    public CapturePointerModifierImpl(int pointerId)
    {
        _pointerId = pointerId;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    ) => element.CapturePointer(_pointerId);

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    ) => element.ReleasePointer(_pointerId);

    protected override bool Equals(CapturePointerModifierImpl other) => _pointerId == other._pointerId;
    public override int GetHashCode() => HashCode.Combine(_pointerId);
}