// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class SizeModifierImpl : BaseUnityModifier<SizeModifierImpl>
{
    private readonly Optional<Dp> _width;
    private readonly Optional<Dp> _height;

    public SizeModifierImpl(Optional<Dp> width, Optional<Dp> height)
    {
        _width = width;
        _height = height;
    }

    public override void Apply(VisualElement element)
    {
        if (_width.HasValue)
            element.style.width = _width.Value.ToLength();
        if (_height.HasValue)
            element.style.height = _height.Value.ToLength();
    }

    public override void Revert(VisualElement element)
    {
        if (_width.HasValue)
            element.style.width = StyleKeyword.Null;
        if (_height.HasValue)
            element.style.height = StyleKeyword.Null;
    }

    protected override bool Equals(SizeModifierImpl other)
    {
        return _width.Equals(other._width) && _height.Equals(other._height);
    }
}