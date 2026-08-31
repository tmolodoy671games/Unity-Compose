// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Clip(
        this IModifier modifier,
        Optional<RoundedCornerShape> shape = default
    )
    {
        return modifier + new ClipModifierImpl(shape);
    }
}

internal class ClipModifierImpl : BaseModifier<ClipModifierImpl>
{
    private readonly Optional<RoundedCornerShape> _shape;

    public ClipModifierImpl(Optional<RoundedCornerShape> shape)
    {
        _shape = shape;
    }

    public override void Apply(VisualElement element)
    {
        element.style.overflow = Overflow.Hidden;
        if (!_shape.HasValue)
            return;
        var shapeValue = _shape.Value;
        element.style.borderTopLeftRadius = shapeValue.TopLeft.ToLength();
        element.style.borderTopRightRadius = shapeValue.TopRight.ToLength();
        element.style.borderBottomLeftRadius = shapeValue.BottomLeft.ToLength();
        element.style.borderBottomRightRadius = shapeValue.BottomRight.ToLength();
    }

    public override void Revert(VisualElement element)
    {
        element.style.overflow = StyleKeyword.Null;
        if (!_shape.HasValue)
            return;
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