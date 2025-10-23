// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IRowScope
{
    IModifier Align(Alignment.Vertical align);

    IModifier FillMaxWidth();
    IModifier Weight(float fraction);
}

internal class RowScopeImpl : IRowScope
{
    public IModifier Align(Alignment.Vertical align)
    {
        return Modifier + new VerticalAlignModifierImpl(align);
    }

    public IModifier FillMaxWidth()
    {
        return Modifier + new WeightModifierImpl(1);
    }

    public IModifier Weight(float fraction)
    {
        return Modifier + new WeightModifierImpl(fraction);
    }
}

internal class VerticalAlignModifierImpl : BaseModifier<VerticalAlignModifierImpl>
{
    private readonly Alignment.Vertical _align;

    public VerticalAlignModifierImpl(Alignment.Vertical align)
    {
        _align = align;
    }

    public override void Apply(VisualElement element)
    {
        element.style.alignSelf = _align switch
        {
            Alignment.Vertical.Top => Align.FlexStart,
            Alignment.Vertical.Center => Align.Center,
            Alignment.Vertical.Bottom => Align.FlexEnd,
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

    protected override bool Equals(VerticalAlignModifierImpl other)
    {
        return _align == other._align;
    }
}