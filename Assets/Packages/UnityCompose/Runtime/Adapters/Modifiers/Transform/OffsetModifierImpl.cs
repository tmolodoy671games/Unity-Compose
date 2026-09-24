// ReSharper disable CheckNamespace

using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class OffsetModifierImpl : BaseUnityModifier<OffsetModifierImpl>
{
    private readonly Dp _x;
    private readonly Dp _y;

    public OffsetModifierImpl(Dp x, Dp y)
    {
        _x = x;
        _y = y;
    }

    public override void Apply(VisualElement element)
    {
        element.style.translate = new Translate(_x.ToLength(), _y.ToLength());
    }

    public override void Revert(VisualElement element)
    {
        element.style.translate = StyleKeyword.Null;
    }

    protected override bool Equals(OffsetModifierImpl other)
    {
        return _x.Equals(other._x) && _y.Equals(other._y);
    }

    public override string ToString()
    {
        return $"Offset({_x}, {_y})";
    }
}