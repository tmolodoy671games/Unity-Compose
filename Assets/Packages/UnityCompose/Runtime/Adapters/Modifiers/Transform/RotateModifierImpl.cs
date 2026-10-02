// ReSharper disable CheckNamespace

using SharpExtensions;
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
        StyleRotate newRotate = new Rotate(element.style.rotate.value.angle.value + _degrees);
        if (newRotate.value.angle.value == 0f)
            newRotate = StyleKeyword.Null;
        element.style.rotate = newRotate;
    }

    public override void Revert(VisualElement element)
    {
        StyleRotate newRotate = new Rotate(element.style.rotate.value.angle.value - _degrees);
        if (newRotate.value.angle.value == 0f)
            newRotate = StyleKeyword.Null;
        element.style.rotate = newRotate;
        
    }

    protected override bool Equals(RotateModifierImpl other)
    {
        return _degrees.AlmostEquals(other._degrees);
    }
}