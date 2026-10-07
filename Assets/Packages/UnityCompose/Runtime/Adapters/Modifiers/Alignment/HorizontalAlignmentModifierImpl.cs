using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal record HorizontalAlignModifierImpl(
    Alignment.Horizontal Align
) : UnityModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (element.parent.NotNull().style.flexDirection.value is not (FlexDirection.Column
            or FlexDirection.ColumnReverse)) return;
        element.style.alignSelf = Align.ToAlign();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (element.parent.NotNull().style.flexDirection.value is not (FlexDirection.Column
            or FlexDirection.ColumnReverse)) return;
        element.style.alignSelf = element.style.alignSelf.CompareAndSetNull(Align.ToAlign());
    }
}