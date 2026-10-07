using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal record VerticalAlignModifierImpl(
    Alignment.Vertical Align
) : UnityModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (element.parent.NotNull().style.flexDirection.value is not (FlexDirection.Row or FlexDirection.RowReverse))
            return;
        element.style.alignSelf = Align.ToAlign();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (element.parent.NotNull().style.flexDirection.value is not (FlexDirection.Row or FlexDirection.RowReverse))
            return;
        element.style.alignSelf = StyleKeyword.Null;
    }
}