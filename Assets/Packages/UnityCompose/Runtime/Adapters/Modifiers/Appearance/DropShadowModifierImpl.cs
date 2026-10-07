// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Linq;
using Compose.Net;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record DropShadowModifierImpl(
    Shape Shape,
    Compose.Net.Shadow Shadow
) : BackgroundShadowUnityModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        Init(drawBehind, Shape, Shadow);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        Revert(drawBehind, Shape, Shadow);
    }

    private static void Init(
        VisualElement shadowElement,
        Shape shape,
        Compose.Net.Shadow shadow
    )
    {
        var style = shadowElement.style;
        style.borderTopLeftRadius = shape.TopLeft.ToLength();
        style.borderTopRightRadius = shape.TopRight.ToLength();
        style.borderBottomLeftRadius = shape.BottomLeft.ToLength();
        style.borderBottomRightRadius = shape.BottomRight.ToLength();
        style.backgroundColor = shadow.Color.ToUnityColor();
        style.translate = style.translate.Add(shadow.Offset.ToVector2());

        style.top = style.top.Subtract(shadow.Spread.ToLength());
        style.bottom = style.bottom.Subtract(shadow.Spread.ToLength());
        style.left = style.left.Subtract(shadow.Spread.ToLength());
        style.right = style.right.Subtract(shadow.Spread.ToLength());

        var blur = new FilterFunction(FilterFunctionType.Blur);
        blur.AddParameter(new FilterParameter(shadow.Radius.Value));
        style.AddFilter(blur);
    }

    private static void Revert(
        VisualElement shadowElement,
        Shape shape,
        Compose.Net.Shadow shadow
    )
    {
        var style = shadowElement.style;
        style.borderTopLeftRadius = style.borderTopLeftRadius.CompareAndSetNull(shape.TopLeft.ToLength());
        style.borderTopRightRadius = style.borderTopRightRadius.CompareAndSetNull(shape.TopRight.ToLength());
        style.borderBottomLeftRadius = style.borderBottomLeftRadius.CompareAndSetNull(shape.BottomLeft.ToLength());
        style.borderBottomRightRadius = style.borderBottomRightRadius.CompareAndSetNull(shape.BottomRight.ToLength());
        style.backgroundColor = style.backgroundColor.CompareAndSetNull(shadow.Color.ToUnityColor());
        style.translate = style.translate.Subtract(shadow.Offset.ToVector2());

        style.top = style.top.Add(shadow.Spread.ToLength());
        style.bottom = style.bottom.Add(shadow.Spread.ToLength());
        style.left = style.left.Add(shadow.Spread.ToLength());
        style.right = style.right.Add(shadow.Spread.ToLength());

        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(shadow.Radius.Value));
        style.RemoveFilter(filter);
    }
}