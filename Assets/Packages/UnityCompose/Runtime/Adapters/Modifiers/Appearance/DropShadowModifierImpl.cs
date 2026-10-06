// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DropShadowModifierImpl : BackgroundShadowUnityModifier<DropShadowModifierImpl>, IAppearanceModifier
{
    private readonly Shape _shape;
    private readonly Compose.Net.Shadow _shadow;

    public DropShadowModifierImpl(Shape shape, Compose.Net.Shadow shadow)
    {
        _shape = shape;
        _shadow = shadow;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        Init(drawBehind, _shape, _shadow);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        Revert(drawBehind);
    }

    protected override bool Equals(DropShadowModifierImpl other)
    {
        return _shadow.Equals(other._shadow) && _shape.Equals(other._shape);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_shape, _shadow);
    }

    private static void Init(
        VisualElement shadowElement,
        Shape shape,
        Compose.Net.Shadow shadow
    )
    {
        shadowElement.style.borderTopLeftRadius = shape.TopLeft.ToLength();
        shadowElement.style.borderTopRightRadius = shape.TopRight.ToLength();
        shadowElement.style.borderBottomLeftRadius = shape.BottomLeft.ToLength();
        shadowElement.style.borderBottomRightRadius = shape.BottomRight.ToLength();
        shadowElement.style.backgroundColor = shadow.Color.ToUnityColor();
        shadowElement.style.translate = shadow.Offset.ToVector2();

        shadowElement.style.top = (-shadow.Spread).ToLength();
        shadowElement.style.bottom = (-shadow.Spread).ToLength();
        shadowElement.style.left = (-shadow.Spread).ToLength();
        shadowElement.style.right = (-shadow.Spread).ToLength();

        var blur = new FilterFunction(FilterFunctionType.Blur);
        blur.AddParameter(new FilterParameter(shadow.Radius.Value));
        shadowElement.style.filter = new List<FilterFunction> { blur };
    }

    private static void Revert(
        VisualElement shadowElement
    )
    {
        shadowElement.style.borderTopLeftRadius = StyleKeyword.Null;
        shadowElement.style.borderTopRightRadius = StyleKeyword.Null;
        shadowElement.style.borderBottomLeftRadius = StyleKeyword.Null;
        shadowElement.style.borderBottomRightRadius = StyleKeyword.Null;
        shadowElement.style.backgroundColor = StyleKeyword.Null;
        shadowElement.style.translate = StyleKeyword.Null;

        shadowElement.style.top = 0;
        shadowElement.style.bottom = 0;
        shadowElement.style.left = 0;
        shadowElement.style.right = 0;

        shadowElement.style.filter = shadowElement.style.filter.value
            .Where(it => it.type != FilterFunctionType.Blur)
            .ToList();
    }
}