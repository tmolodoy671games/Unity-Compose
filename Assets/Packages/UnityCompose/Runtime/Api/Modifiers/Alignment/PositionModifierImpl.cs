using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Position(
        this IModifier modifier,
        LayoutCoordinate top = default,
        LayoutCoordinate bottom = default,
        LayoutCoordinate left = default,
        LayoutCoordinate right = default
    )
    {
        return modifier + new PositionModifierImpl(top, bottom, left, right);
    }
}

internal class PositionModifierImpl : BaseModifier<PositionModifierImpl>
{
    private readonly LayoutCoordinate _top;
    private readonly LayoutCoordinate _bottom;
    private readonly LayoutCoordinate _left;
    private readonly LayoutCoordinate _right;

    public PositionModifierImpl(
        LayoutCoordinate top,
        LayoutCoordinate bottom,
        LayoutCoordinate left,
        LayoutCoordinate right
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

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        if (_top.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.Top);
        if (_bottom.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.Bottom);
        if (_left.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.Left);
        if (_right.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.Right);
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