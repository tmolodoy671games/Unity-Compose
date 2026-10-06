// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DropShadowModifierImpl : UnityModifier<DropShadowModifierImpl>, IAppearanceModifier
{
    private readonly RoundedCornerShape _shape;
    private readonly Compose.Net.Shadow _shadow;

    public DropShadowModifierImpl(RoundedCornerShape shape, Compose.Net.Shadow shadow)
    {
        _shape = shape;
        _shadow = shadow;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var shadow = node.SetupDrawBehind();
        Init(shadow, _shape, _shadow);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        foreach (var newModifier in newModifiers)
        {
            if (newModifier is DropShadowModifierImpl)
                return;
        }
        node.RemoveDrawBehind();
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
        RoundedCornerShape shape,
        Compose.Net.Shadow shadow
    )
    {
        shadowElement.style.position = Position.Absolute;
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
}