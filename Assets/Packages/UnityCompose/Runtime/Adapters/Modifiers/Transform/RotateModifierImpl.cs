// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class RotateModifierImpl : BaseUnityModifier<RotateModifierImpl>
{
    private readonly float _degrees;

    public RotateModifierImpl(float degrees)
    {
        _degrees = degrees;
    }

    public override void Apply(VisualElement element)
    {
        element.style.rotate = new Rotate(_degrees);
    }

    public override void Revert(VisualElement element)
    {
        element.style.rotate = StyleKeyword.Null;
    }

    protected override bool Equals(RotateModifierImpl other)
    {
        return _degrees.AlmostEquals(other._degrees);
    }
}