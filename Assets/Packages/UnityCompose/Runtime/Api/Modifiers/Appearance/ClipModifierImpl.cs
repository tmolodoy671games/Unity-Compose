// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Clip(
        this IModifier modifier,
        Optional<RoundedCornerShape> shape = default,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new ClipModifierImpl(shape, transition);
    }
}

internal class ClipModifierImpl : BaseModifier<ClipModifierImpl>
{
    private readonly Optional<RoundedCornerShape> _shape;
    private readonly Optional<ComposeTransition> _transition;

    public ClipModifierImpl(Optional<RoundedCornerShape> shape, Optional<ComposeTransition> transition)
    {
        _shape = shape;
        _transition = transition;
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
        if (_transition.HasValue)
        {
            var transitionValue = _transition.Value;
            element.AddTransitions(
                transitionValue,
                "border-top-left-radius",
                "border-top-right-radius",
                "border-bottom-left-radius",
                "border-bottom-right-radius"
            );
        }
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
        if (_transition.HasValue)
        {
            element.RemoveTransitions(
                "border-top-left-radius",
                "border-top-right-radius",
                "border-bottom-left-radius",
                "border-bottom-right-radius"
            );
        }
    }

    protected override bool Equals(ClipModifierImpl other)
    {
        return _shape.Equals(other._shape) && _transition.Equals(other._transition);
    }
}