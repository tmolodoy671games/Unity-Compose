using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class HorizontalAlignModifierImpl : UnityModifier<HorizontalAlignModifierImpl>
{
    private readonly Alignment.Horizontal _align;

    public HorizontalAlignModifierImpl(Alignment.Horizontal align)
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
            case FlexDirection.Column:
            case FlexDirection.ColumnReverse:
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
            case FlexDirection.Column:
            case FlexDirection.ColumnReverse:
                element.style.alignSelf = StyleKeyword.Null;
                break;
        }
    }

    protected override bool Equals(HorizontalAlignModifierImpl other) => _align == other._align;
    public override int GetHashCode() => HashCode.Combine(_align);
}