// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class PaddingModifierImpl : BaseUnityModifier<PaddingModifierImpl>
{
    private readonly Optional<Dp> _top;
    private readonly Optional<Dp> _bottom;
    private readonly Optional<Dp> _left;
    private readonly Optional<Dp> _right;

    public PaddingModifierImpl(
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
            element.style.paddingTop = _top.Value.ToLength();
        }

        if (_bottom.HasValue)
        {
            element.style.paddingBottom = _bottom.Value.ToLength();
        }

        if (_left.HasValue)
        {
            element.style.paddingLeft = _left.Value.ToLength();
        }

        if (_right.HasValue)
        {
            element.style.paddingRight = _right.Value.ToLength();
        }
    }

    public override void Revert(VisualElement element)
    {
        if (_top.HasValue)
        {
            element.style.paddingTop = StyleKeyword.Null;
        }

        if (_bottom.HasValue)
        {
            element.style.paddingBottom = StyleKeyword.Null;
        }

        if (_left.HasValue)
        {
            element.style.paddingLeft = StyleKeyword.Null;
        }

        if (_right.HasValue)
        {
            element.style.paddingRight = StyleKeyword.Null;
        }
    }

    protected override bool Equals(PaddingModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}