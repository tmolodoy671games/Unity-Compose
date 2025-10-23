// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IBoxScope
{
    IModifier Align(HorizontalAlign align);

    IModifier FillMaxWidth();
    IModifier FillMaxHeight();

    IModifier Position(
        Optional<float> top,
        Optional<float> bottom,
        Optional<float> left,
        Optional<float> right
    );
}

internal class BoxScopeImpl : IBoxScope
{
    public IModifier Align(HorizontalAlign align)
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
        Optional<float> top,
        Optional<float> bottom,
        Optional<float> left,
        Optional<float> right
    )
    {
        return Modifier + new BoxPositionModifierImpl(top, bottom, left, right);
    }
}

internal class HorizontalAlignModifierImpl : BaseModifier<HorizontalAlignModifierImpl>
{
    private readonly HorizontalAlign _align;

    public HorizontalAlignModifierImpl(HorizontalAlign align)
    {
        _align = align;
    }

    public override void Apply(VisualElement element)
    {
        element.style.alignSelf = _align switch
        {
            HorizontalAlign.Left => Align.FlexStart,
            HorizontalAlign.Center => Align.Center,
            HorizontalAlign.Right => Align.FlexEnd,
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
    private readonly Optional<float> _top;
    private readonly Optional<float> _bottom;
    private readonly Optional<float> _left;
    private readonly Optional<float> _right;

    public BoxPositionModifierImpl(
        Optional<float> top,
        Optional<float> bottom,
        Optional<float> left,
        Optional<float> right
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
            element.style.top = _top.Value;
        if (_bottom.HasValue)
            element.style.bottom = _bottom.Value;
        if (_left.HasValue)
            element.style.left = _left.Value;
        if (_right.HasValue)
            element.style.right = _right.Value;
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