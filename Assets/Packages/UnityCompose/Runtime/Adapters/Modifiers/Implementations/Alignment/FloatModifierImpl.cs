// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;


internal class FloatModifierImpl : BaseUnityModifier<FloatModifierImpl>
{
    public static readonly FloatModifierImpl Instance = new();

    private FloatModifierImpl()
    {
    }

    public override void Apply(VisualElement element)
    {
        element.style.position = Position.Absolute;
    }

    public override void Revert(VisualElement element)
    {
        element.style.position = StyleKeyword.Null;
    }

    protected override bool Equals(FloatModifierImpl other)
    {
        return true;
    }

    public override string ToString()
    {
        return "Float";
    }
}