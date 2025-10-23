// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IBoxScope
{
    IModifier Align(Alignment.Horizontal align);

    IModifier FillMaxWidth();
    IModifier FillMaxHeight();

    IModifier Position(
        LayoutCoordinate top = default,
        LayoutCoordinate bottom = default,
        LayoutCoordinate left = default,
        LayoutCoordinate right = default
    );
}

internal class BoxScopeImpl : IBoxScope
{
    public IModifier Align(Alignment.Horizontal align)
    {
        return Modifier + new HorizontalAlignModifierImpl(align);
    }

    public IModifier FillMaxWidth()
    {
        return Modifier + FillMaxWidthModifierImpl.Instance;
    }

    public IModifier FillMaxHeight()
    {
        return Modifier + new WeightModifierImpl(1);
    }

    public IModifier Position(
        LayoutCoordinate top,
        LayoutCoordinate bottom,
        LayoutCoordinate left,
        LayoutCoordinate right
    )
    {
        return Modifier + new BoxPositionModifierImpl(top, bottom, left, right);
    }
}

internal class HorizontalAlignModifierImpl : BaseModifier<HorizontalAlignModifierImpl>
{
    private readonly Alignment.Horizontal _align;

    public HorizontalAlignModifierImpl(Alignment.Horizontal align)
    {
        _align = align;
    }

    public override void Apply(VisualElement element)
    {
        element.style.alignSelf = _align switch
        {
            Alignment.Horizontal.Left => Align.FlexStart,
            Alignment.Horizontal.Center => Align.Center,
            Alignment.Horizontal.Right => Align.FlexEnd,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.AlignSelf);
    }

    public override void Revert(VisualElement element)
    {
        element.style.alignSelf = StyleKeyword.Null;
    }

    protected override bool Equals(HorizontalAlignModifierImpl other)
    {
        return _align == other._align;
    }
}

internal class FillMaxWidthModifierImpl : BaseModifier<FillMaxWidthModifierImpl>
{
    public static readonly FillMaxWidthModifierImpl Instance = new();

    private FillMaxWidthModifierImpl()
    {
    }

    public override void Apply(VisualElement element)
    {
        element.style.alignSelf = Align.Stretch;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.AlignSelf);
    }

    public override void Revert(VisualElement element)
    {
        element.style.alignSelf = StyleKeyword.Null;
    }

    protected override bool Equals(FillMaxWidthModifierImpl other)
    {
        return true;
    }
}

internal class BoxPositionModifierImpl : BaseModifier<BoxPositionModifierImpl>
{
    private readonly LayoutCoordinate _top;
    private readonly LayoutCoordinate _bottom;
    private readonly LayoutCoordinate _left;
    private readonly LayoutCoordinate _right;

    public BoxPositionModifierImpl(
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

    protected override bool Equals(BoxPositionModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}