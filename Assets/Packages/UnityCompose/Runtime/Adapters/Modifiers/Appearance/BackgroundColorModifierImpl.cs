// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class BackgroundColorModifierImpl : BaseUnityModifier<BackgroundColorModifierImpl>
{
    private readonly Color _backgroundColor;
    private readonly Optional<RoundedCornerShape> _shape;

    public BackgroundColorModifierImpl(
        Color backgroundColor,
        Optional<RoundedCornerShape> shape
    )
    {
        _backgroundColor = backgroundColor;
        _shape = shape;
    }

    public override void Apply(VisualElement element)
    {
        element.style.backgroundColor = _backgroundColor;
        if (_shape.HasValue)
        {
            var shapeValue = _shape.Value;
            element.style.borderTopLeftRadius = shapeValue.TopLeft.ToLength();
            element.style.borderTopRightRadius = shapeValue.TopRight.ToLength();
            element.style.borderBottomLeftRadius = shapeValue.BottomLeft.ToLength();
            element.style.borderBottomRightRadius = shapeValue.BottomRight.ToLength();
        }
    }

    public override void Revert(VisualElement element)
    {
        element.style.backgroundColor = StyleKeyword.Null;
        if (_shape.HasValue)
        {
            element.style.borderTopLeftRadius = StyleKeyword.Null;
            element.style.borderTopRightRadius = StyleKeyword.Null;
            element.style.borderBottomLeftRadius = StyleKeyword.Null;
            element.style.borderBottomRightRadius = StyleKeyword.Null;
        }
    }

    protected override bool Equals(BackgroundColorModifierImpl other)
    {
        return _backgroundColor == other._backgroundColor &&
               _shape.Equals(other._shape);
    }
}