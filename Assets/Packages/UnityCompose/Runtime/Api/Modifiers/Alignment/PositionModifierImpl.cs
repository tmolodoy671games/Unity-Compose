using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Position(
        this IModifier modifier,
        LayoutLength top = default,
        LayoutLength bottom = default,
        LayoutLength left = default,
        LayoutLength right = default
    )
    {
        return modifier + new PositionModifierImpl(top, bottom, left, right);
    }
}

internal class PositionModifierImpl : BaseModifier<PositionModifierImpl>
{
    private readonly LayoutLength _top;
    private readonly LayoutLength _bottom;
    private readonly LayoutLength _left;
    private readonly LayoutLength _right;

    public PositionModifierImpl(
        LayoutLength top,
        LayoutLength bottom,
        LayoutLength left,
        LayoutLength right
    )
    {
        _top = top;
        _bottom = bottom;
        _left = left;
        _right = right;
    }

    public override void Apply(VisualElement element)
    {
        element.style.position = Position.Absolute;
        if (_top.HasValue)
            element.style.top = _top.ToLength();
        if (_bottom.HasValue)
            element.style.bottom = _bottom.ToLength();
        if (_left.HasValue)
            element.style.left = _left.ToLength();
        if (_right.HasValue)
            element.style.right = _right.ToLength();
    }

    public override void Revert(VisualElement element)
    {
        if (_top.HasValue)
            element.style.top = StyleKeyword.Null;
        if (_bottom.HasValue)
            element.style.bottom = StyleKeyword.Null;
        if (_left.HasValue)
            element.style.left = StyleKeyword.Null;
        if (_right.HasValue)
            element.style.right = StyleKeyword.Null;
    }

    protected override bool Equals(PositionModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}