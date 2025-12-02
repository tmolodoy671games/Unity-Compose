// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier FillMaxSize(this IModifier modifier, float fraction = 1)
    {
        return modifier + new FillMaxSizeModifierImpl(fraction, fraction);
    }

    public static IModifier FillMaxWidth(this IModifier modifier, float fraction = 1)
    {
        return modifier + new FillMaxSizeModifierImpl(fraction, -1);
    }

    public static IModifier FillMaxHeight(this IModifier modifier, float fraction = 1)
    {
        return modifier + new FillMaxSizeModifierImpl(-1, fraction);
    }
}

internal class FillMaxSizeModifierImpl : BaseModifier<FillMaxSizeModifierImpl>
{
    private readonly float _widthFraction;
    private readonly float _heightFraction;

    public FillMaxSizeModifierImpl(float widthFraction, float heightFraction)
    {
        _widthFraction = widthFraction;
        _heightFraction = heightFraction;
    }

    public override void Apply(VisualElement element)
    {
        if (_widthFraction > 0)
            element.style.width = new Length(_widthFraction * 100, LengthUnit.Percent);
        if (_heightFraction > 0)
            element.style.height = new Length(_heightFraction * 100, LengthUnit.Percent);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        if (_widthFraction > 0)
            modifiedProperties.Add(ComposeModifiedProperty.Width);
        if (_heightFraction > 0)
            modifiedProperties.Add(ComposeModifiedProperty.Height);
    }

    public override void Revert(VisualElement element)
    {
        if (_widthFraction > 0)
            element.style.width = StyleKeyword.Null;
        if (_heightFraction > 0)
            element.style.height = StyleKeyword.Null;
    }

    protected override bool Equals(FillMaxSizeModifierImpl other)
    {
        return _widthFraction.AlmostEquals(other._widthFraction) &&
               _heightFraction.AlmostEquals(other._heightFraction);
    }
}