// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class SizeInModifierImpl : BaseUnityModifier<SizeInModifierImpl>
{
    private readonly Optional<Dp> _minWidth;
    private readonly Optional<Dp> _maxWidth;
    private readonly Optional<Dp> _minHeight;
    private readonly Optional<Dp> _maxHeight;

    public SizeInModifierImpl(
        Optional<Dp> minWidth,
        Optional<Dp> maxWidth,
        Optional<Dp> minHeight,
        Optional<Dp> maxHeight
    )
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
    }

    public override void Apply(VisualElement element)
    {
        if (_minWidth.HasValue)
            element.style.minWidth = _minWidth.Value.ToLength();
        if (_maxWidth.HasValue)
            element.style.maxWidth = _maxWidth.Value.ToLength();
        if (_minHeight.HasValue)
            element.style.minHeight = _minHeight.Value.ToLength();
        if (_maxHeight.HasValue)
            element.style.maxHeight = _maxHeight.Value.ToLength();
    }

    public override void Revert(VisualElement element)
    {
        if (_minWidth.HasValue)
            element.style.minWidth = StyleKeyword.Null;
        if (_maxWidth.HasValue)
            element.style.maxWidth = StyleKeyword.Null;
        if (_minHeight.HasValue)
            element.style.minHeight = StyleKeyword.Null;
        if (_maxHeight.HasValue)
            element.style.maxHeight = StyleKeyword.Null;
    }

    protected override bool Equals(SizeInModifierImpl other)
    {
        return _minWidth.Equals(other._minWidth) &&
               _maxWidth.Equals(other._maxWidth) &&
               _minHeight.Equals(other._minHeight) &&
               _maxHeight.Equals(other._maxHeight);
    }
}