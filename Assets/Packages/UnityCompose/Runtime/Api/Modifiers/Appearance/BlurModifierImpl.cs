// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Blur(this IModifier modifier, float strength = 1f)
    {
        if (strength.AlmostEquals(0f))
            return modifier;
        return modifier + new BlurModifierImpl(strength);
    }
}

internal class BlurModifierImpl : BaseModifier<BlurModifierImpl>
{
    private readonly float _strength;

    public BlurModifierImpl(float strength)
    {
        _strength = strength;
    }

    public override void Apply(VisualElement element)
    {
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(_strength));
        if (element.style.filter.value == null)
            element.style.filter = new List<FilterFunction>();
        element.style.filter.value.Add(filter);
        element.style.filter = element.style.filter.value.ToList();
    }

    public override void Revert(VisualElement element)
    {
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(_strength));
        if (element.style.filter.value == null)
            element.style.filter = new List<FilterFunction>();
        element.style.filter.value.Remove(filter);
        element.style.filter = element.style.filter.value.ToList();
    }

    protected override bool Equals(BlurModifierImpl other)
    {
        return _strength.AlmostEquals(other._strength);
    }
}