using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class AlignModifierImpl : UnityModifier<AlignModifierImpl>
{
    private readonly Alignment _align;

    public AlignModifierImpl(Alignment align)
    {
        _align = align;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = _align.ToAlign();
                break;
        }
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = StyleKeyword.Null;
                break;
        }
    }

    protected override bool Equals(AlignModifierImpl other) => _align == other._align;
    public override int GetHashCode() => HashCode.Combine(_align);
}