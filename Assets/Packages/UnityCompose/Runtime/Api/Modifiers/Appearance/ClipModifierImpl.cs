// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Clip(
        this IModifier modifier,
        RoundedCornerShape shape
    )
    {
        return modifier + new ClipModifierImpl(shape);
    }
}

internal class ClipModifierImpl : BaseModifier<ClipModifierImpl>
{
    private readonly RoundedCornerShape _shape;

    public ClipModifierImpl(RoundedCornerShape shape)
    {
        _shape = shape;
    }

    public override void Apply(VisualElement element)
    {
        element.style.overflow = Overflow.Hidden;
        element.style.borderTopLeftRadius = _shape.TopLeft.ToLength();
        element.style.borderTopRightRadius = _shape.TopRight.ToLength();
        element.style.borderBottomLeftRadius = _shape.BottomLeft.ToLength();
        element.style.borderBottomRightRadius = _shape.BottomRight.ToLength();
    }

    public override void Revert(VisualElement element)
    {
        element.style.overflow = StyleKeyword.Null;
        element.style.borderTopLeftRadius = StyleKeyword.Null;
        element.style.borderTopRightRadius = StyleKeyword.Null;
        element.style.borderBottomLeftRadius = StyleKeyword.Null;
        element.style.borderBottomRightRadius = StyleKeyword.Null;
    }

    protected override bool Equals(ClipModifierImpl other)
    {
        return true;
    }
}