// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class MarginModifierImpl : BaseUnityModifier<MarginModifierImpl>
{
    private readonly Optional<Dp> _top;
    private readonly Optional<Dp> _bottom;
    private readonly Optional<Dp> _left;
    private readonly Optional<Dp> _right;

    public MarginModifierImpl(
        Optional<Dp> top,
        Optional<Dp> bottom,
        Optional<Dp> left,
        Optional<Dp> right
    )
    {
        _top = top;
        _bottom = bottom;
        _left = left;
        _right = right;
    }

    public override void Apply(VisualElement element)
    {
        if (_top.HasValue)
        {
            element.style.marginTop = _top.Value.ToLength();
        }

        if (_bottom.HasValue)
        {
            element.style.marginBottom = _bottom.Value.ToLength();
        }

        if (_left.HasValue)
        {
            element.style.marginLeft = _left.Value.ToLength();
        }

        if (_right.HasValue)
        {
            element.style.marginRight = _right.Value.ToLength();
        }
    }

    public override void Revert(VisualElement element)
    {
        if (_top.HasValue)
        {
            element.style.marginTop = StyleKeyword.Null;
        }

        if (_bottom.HasValue)
        {
            element.style.marginBottom = StyleKeyword.Null;
        }

        if (_left.HasValue)
        {
            element.style.marginLeft = StyleKeyword.Null;
        }

        if (_right.HasValue)
        {
            element.style.marginRight = StyleKeyword.Null;
        }
    }

    protected override bool Equals(MarginModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}