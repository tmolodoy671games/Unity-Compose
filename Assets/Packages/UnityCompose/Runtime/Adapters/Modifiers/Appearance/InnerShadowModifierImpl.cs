// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class InnerShadowModifierImpl : ForegroundShadowUnityModifier<InnerShadowModifierImpl>, IAppearanceModifier
{
    private readonly RoundedCornerShape _shape;
    private readonly Compose.Net.Shadow _shadow;

    public InnerShadowModifierImpl(RoundedCornerShape shape, Compose.Net.Shadow shadow)
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
        element.style.overflow = Overflow.Hidden;
        Init(drawBehind, _shape, _shadow);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawBehind,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.overflow = StyleKeyword.Null;
        Revert(drawBehind);
    }

    protected override bool Equals(InnerShadowModifierImpl other)
    {
        return _shadow.Equals(other._shadow) && _shape.Equals(other._shape);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_shape, _shadow);
    }

    private static void Init(
        VisualElement shadowElement,
        RoundedCornerShape shape,
        Compose.Net.Shadow shadow
    )
    {
        shadowElement.style.borderTopLeftRadius = shape.TopLeft.ToLength();
        shadowElement.style.borderTopRightRadius = shape.TopRight.ToLength();
        shadowElement.style.borderBottomLeftRadius = shape.BottomLeft.ToLength();
        shadowElement.style.borderBottomRightRadius = shape.BottomRight.ToLength();
        
        shadowElement.style.borderTopColor = shadow.Color.ToUnityColor();
        shadowElement.style.borderBottomColor = shadow.Color.ToUnityColor();
        shadowElement.style.borderLeftColor = shadow.Color.ToUnityColor();
        shadowElement.style.borderRightColor = shadow.Color.ToUnityColor();

        var borderWidth = shadow.Spread * 2;
        shadowElement.style.borderTopWidth = borderWidth.Value;
        shadowElement.style.borderBottomWidth = borderWidth.Value;
        shadowElement.style.borderLeftWidth = borderWidth.Value;
        shadowElement.style.borderRightWidth = borderWidth.Value;

        var offsetWidth = (-borderWidth / 2).ToLength();
        shadowElement.style.top = offsetWidth;
        shadowElement.style.bottom = offsetWidth;
        shadowElement.style.left = offsetWidth;
        shadowElement.style.right = offsetWidth;
        
        shadowElement.style.translate = shadow.Offset.ToVector2();

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
        
        shadowElement.style.borderTopColor = StyleKeyword.Null;
        shadowElement.style.borderBottomColor = StyleKeyword.Null;
        shadowElement.style.borderLeftColor = StyleKeyword.Null;
        shadowElement.style.borderRightColor = StyleKeyword.Null;
        
        shadowElement.style.borderTopWidth = StyleKeyword.Null;
        shadowElement.style.borderBottomWidth = StyleKeyword.Null;
        shadowElement.style.borderLeftWidth = StyleKeyword.Null;
        shadowElement.style.borderRightWidth = StyleKeyword.Null;
        
        shadowElement.style.translate = StyleKeyword.Null;

        shadowElement.style.filter = shadowElement.style.filter.value
            .Where(it => it.type != FilterFunctionType.Blur)
            .ToList();
    }
}