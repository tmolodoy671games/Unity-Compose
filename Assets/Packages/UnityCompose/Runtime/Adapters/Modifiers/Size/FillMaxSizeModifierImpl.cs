// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class FillMaxSizeModifierImpl : BaseUnityModifier<FillMaxSizeModifierImpl>
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