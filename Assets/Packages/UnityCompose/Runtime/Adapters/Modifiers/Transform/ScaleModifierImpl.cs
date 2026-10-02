// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class ScaleModifierImpl : BaseUnityModifier<ScaleModifierImpl>
{
    private readonly float _scaleX;
    private readonly float _scaleY;

    public ScaleModifierImpl(float scaleX, float scaleY)
    {
        _scaleX = scaleX;
        _scaleY = scaleY;
    }

    public override void Apply(VisualElement element)
    {
        var newScale = element.style.scale.value.value.ToVector2() + new Vector2(_scaleX, _scaleY);
        var newScaleValue = newScale == Vector2.zero ? StyleKeyword.Null : new StyleScale(newScale);
        element.style.scale = newScaleValue;
    }

    public override void Revert(VisualElement element)
    {
        var newScale = element.style.scale.value.value.ToVector2() - new Vector2(_scaleX, _scaleY);
        var newScaleValue = newScale == Vector2.zero ? StyleKeyword.Null : new StyleScale(newScale);
        element.style.scale = newScaleValue;
    }

    protected override bool Equals(ScaleModifierImpl other)
    {
        return _scaleX.AlmostEquals(other._scaleX) &&
               _scaleY.AlmostEquals(other._scaleY);
    }
}