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
        var visualElement = node.VisualElement();
        var root = new ShadowWrapper
        {
            style =
            {
                marginLeft = visualElement.style.marginLeft,
                marginRight = visualElement.style.marginRight,
                marginTop = visualElement.style.marginTop,
                marginBottom = visualElement.style.marginBottom
            }
        };
        visualElement.style.marginLeft = StyleKeyword.Null;
        visualElement.style.marginRight = StyleKeyword.Null;
        visualElement.style.marginTop = StyleKeyword.Null;
        visualElement.style.marginBottom = StyleKeyword.Null;

        var shadow = new Shadow().Init(_shape, _shadow);
        root.Add(shadow);
        node.CastTo<UnityReusableComposeNode>().SetRoot(
            root
        );
    }

    public override void Revert(IReusableComposeNode node)
    {
        var visualElement = node.VisualElement();
        var root = node.Root();
        visualElement.style.marginLeft = root.style.marginLeft;
        visualElement.style.marginRight = root.style.marginRight;
        visualElement.style.marginTop = root.style.marginTop;
        visualElement.style.marginBottom = root.style.marginBottom;
        node.CastTo<UnityReusableComposeNode>().RemoveRoot();
    }

    protected override bool Equals(DropShadowModifierImpl other)
    {
        return _shadow.Equals(other._shadow) && _shape.Equals(other._shape);
    }
}

internal class ShadowWrapper : VisualElement
{
}

internal class Shadow : VisualElement
{
    public Shadow Init(
        RoundedCornerShape shape,
        Compose.Net.Shadow shadow
    )
    {
        style.position = Position.Absolute;
        style.width = new Length(105, LengthUnit.Percent);
        style.height = new Length(105, LengthUnit.Percent);
        style.borderTopLeftRadius = shape.TopLeft.ToLength();
        style.borderTopRightRadius = shape.TopRight.ToLength();
        style.borderBottomLeftRadius = shape.BottomLeft.ToLength();
        style.borderBottomRightRadius = shape.BottomRight.ToLength();
        style.backgroundColor = shadow.Color.ToUnityColor();
        style.translate = shadow.Offset.ToVector2();
        var blur = new FilterFunction(FilterFunctionType.Blur);
        blur.AddParameter(new FilterParameter(shadow.Radius.Value));
        style.filter = new List<FilterFunction> { blur };
        return this;
    }
}