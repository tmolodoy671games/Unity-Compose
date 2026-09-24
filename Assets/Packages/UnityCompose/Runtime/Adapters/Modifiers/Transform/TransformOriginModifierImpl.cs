// ReSharper disable CheckNamespace

using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class TransformOriginModifierImpl : BaseUnityModifier<TransformOriginModifierImpl>
{
    private readonly Dp _x;
    private readonly Dp _y;

    public TransformOriginModifierImpl(Dp x, Dp y)
    {
        _x = x;
        _y = y;
    }

    public override void Apply(VisualElement element)
    {
        element.style.transformOrigin = new TransformOrigin(_x.ToLength(), _y.ToLength());
    }

    public override void Revert(VisualElement element)
    {
        element.style.transformOrigin = StyleKeyword.Null;
    }

    protected override bool Equals(TransformOriginModifierImpl other)
    {
        return _x == other._x && _y == other._y;
    }
}