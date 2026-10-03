// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class FillMaxSizeModifierImpl : UnityModifier<FillMaxSizeModifierImpl>, IAppearanceModifier
{
    private readonly float _widthFraction;
    private readonly float _heightFraction;

    public FillMaxSizeModifierImpl(float widthFraction, float heightFraction)
    {
        _widthFraction = widthFraction;
        _heightFraction = heightFraction;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (_widthFraction > 0)
            element.style.width = new Length(_widthFraction * 100, LengthUnit.Percent);
        if (_heightFraction > 0)
            element.style.height = new Length(_heightFraction * 100, LengthUnit.Percent);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
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

    public override int GetHashCode() => HashCode.Combine(_widthFraction, _heightFraction);
}