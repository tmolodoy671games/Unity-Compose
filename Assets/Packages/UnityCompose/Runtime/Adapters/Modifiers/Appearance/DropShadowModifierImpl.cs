// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Drawing;
using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class DropShadowModifierImpl : BaseModifier<DropShadowModifierImpl>
{
    private readonly RoundedCornerShape _shape;
    private readonly Compose.Net.Shadow _shadow;

    public DropShadowModifierImpl(RoundedCornerShape shape, Compose.Net.Shadow shadow)
    {
        _shape = shape;
        _shadow = shadow;
    }

    public override void Apply(IReusableComposeNode node)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        var shadow = unityNode.SetupShadow();
        Init(shadow, _shape, _shadow);
    }

    public override void Revert(IReusableComposeNode node)
    {
        node.CastTo<UnityReusableComposeNode>().RemoveShadow();
    }

    protected override bool Equals(DropShadowModifierImpl other)
    {
        return _shadow.Equals(other._shadow) && _shape.Equals(other._shape);
    }

    private static void Init(
        VisualElement shadowElement,
        RoundedCornerShape shape,
        Compose.Net.Shadow shadow
    )
    {
        shadowElement.style.position = Position.Absolute;
        shadowElement.style.width = new Length(100, LengthUnit.Percent);
        shadowElement.style.height = new Length(100, LengthUnit.Percent);
        shadowElement.style.borderTopLeftRadius = shape.TopLeft.ToLength();
        shadowElement.style.borderTopRightRadius = shape.TopRight.ToLength();
        shadowElement.style.borderBottomLeftRadius = shape.BottomLeft.ToLength();
        shadowElement.style.borderBottomRightRadius = shape.BottomRight.ToLength();
        shadowElement.style.backgroundColor = shadow.Color.ToUnityColor();
        shadowElement.style.translate = shadow.Offset.ToVector2();
        var blur = new FilterFunction(FilterFunctionType.Blur);
        blur.AddParameter(new FilterParameter(shadow.Radius.Value));
        shadowElement.style.filter = new List<FilterFunction> { blur };
    }
}