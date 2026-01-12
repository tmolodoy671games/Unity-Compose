using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Align(this IModifier modifier, Alignment.Horizontal horizontalAlignment)
    {
        return modifier + new HorizontalAlignModifierImpl(horizontalAlignment);
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
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Column:
            case FlexDirection.ColumnReverse:
                element.style.alignSelf = _align.ToAlign();
                break;
        }
    }

    public override void Revert(VisualElement element)
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Column:
            case FlexDirection.ColumnReverse:
                element.style.alignSelf = StyleKeyword.Null;
                break;
        }
    }

    protected override bool Equals(HorizontalAlignModifierImpl other)
    {
        return _align == other._align;
    }
}

internal static class HorizontalAlignmentVisualElementExtensions
{
    public static void ApplyHorizontalAlignment(this VisualElement element, Alignment.Horizontal horizontalAlignment)
    {
        if (element.parent.NotNull().style.flexDirection.value is FlexDirection.Column or FlexDirection.ColumnReverse)
            element.style.alignSelf = horizontalAlignment.ToAlign();
    }

    public static void RevertHorizontalAlignment(this VisualElement element)
    {
        if (element.parent.NotNull().style.flexDirection.value is FlexDirection.Column or FlexDirection.ColumnReverse)
            element.style.alignSelf = StyleKeyword.Null;
    }
}