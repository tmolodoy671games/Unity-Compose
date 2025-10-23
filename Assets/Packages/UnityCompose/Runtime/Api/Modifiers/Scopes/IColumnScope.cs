// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IColumnScope
{
    IModifier Align(Alignment.Horizontal align);

    IModifier FillMaxWidth();
    IModifier Weight(float fraction);
}

internal class ColumnScopeImpl : IColumnScope
{
    public IModifier Align(Alignment.Horizontal align)
    {
        return Modifier + new HorizontalAlignModifierImpl(align);
    }

    public IModifier FillMaxWidth()
    {
        return Modifier + FillMaxWidthModifierImpl.Instance;
    }

    public IModifier Weight(float fraction)
    {
        return Modifier + new WeightModifierImpl(fraction);
    }
}

internal class WeightModifierImpl : BaseModifier<WeightModifierImpl>
{
    private readonly float _weight;

    public WeightModifierImpl(float weight)
    {
        _weight = weight;
    }

    public override void Apply(VisualElement element)
    {
        element.style.flexGrow = _weight;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.FlexGrow);
    }

    public override void Revert(VisualElement element)
    {
        element.style.flexGrow = StyleKeyword.Null;
    }

    protected override bool Equals(WeightModifierImpl other)
    {
        return _weight.AlmostEquals(other._weight);
    }
}