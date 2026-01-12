using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Align(this IModifier modifier, Alignment.Vertical verticalAlignment)
    {
        return modifier + new VerticalAlignModifierImpl(verticalAlignment);
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
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = _align.ToAlign();
                break;
        }
    }

    public override void Revert(VisualElement element)
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = StyleKeyword.Null;
                break;
        }
    }

    protected override bool Equals(VerticalAlignModifierImpl other)
    {
        return _align == other._align;
    }
}

internal static class VerticalAlignmentVisualElementExtensions
{
    public static void ApplyVerticalAlignment(this VisualElement element, Alignment.Vertical verticalAlignment)
    {
        if (element.parent.NotNull().style.flexDirection.value is FlexDirection.Row or FlexDirection.RowReverse)
            element.style.alignSelf = verticalAlignment.ToAlign();
    }

    public static void RevertVerticalAlignment(this VisualElement element)
    {
        if (element.parent.NotNull().style.flexDirection.value is FlexDirection.Row or FlexDirection.RowReverse)
            element.style.alignSelf = StyleKeyword.Null;
    }
}