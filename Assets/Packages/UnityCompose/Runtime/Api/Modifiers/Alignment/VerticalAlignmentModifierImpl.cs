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
        switch (CurrentComposer.GetParentVisualElement().NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = _align.ToAlign();
                break;
        }
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        switch (CurrentComposer.GetParentVisualElement().NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                modifiedProperties.Add(ComposeModifiedProperty.AlignSelf);
                break;
        }
    }

    public override void Revert(VisualElement element)
    {
        switch (CurrentComposer.GetParentVisualElement().NotNull().style.flexDirection.value)
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