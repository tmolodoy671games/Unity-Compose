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
        element.style.scale = new Vector2(_scaleX, _scaleY);
    }

    public override void Revert(VisualElement element)
    {
        element.style.scale = StyleKeyword.Null;
    }

    protected override bool Equals(ScaleModifierImpl other)
    {
        return _scaleX.AlmostEquals(other._scaleX) &&
               _scaleY.AlmostEquals(other._scaleY);
    }
}