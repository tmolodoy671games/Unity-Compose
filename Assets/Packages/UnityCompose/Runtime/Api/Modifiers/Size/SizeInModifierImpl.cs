// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier SizeIn(
        this IModifier modifier,
        LayoutLength min = default,
        LayoutLength max = default,
        LayoutLength minWidth = default,
        LayoutLength maxWidth = default,
        LayoutLength minHeight = default,
        LayoutLength maxHeight = default
    )
    {
        return modifier + new SizeInModifierImpl(
            minWidth: ParamUtils.Resolve(minWidth, min),
            maxWidth: ParamUtils.Resolve(maxWidth, max),
            minHeight: ParamUtils.Resolve(minHeight, min),
            maxHeight: ParamUtils.Resolve(maxHeight, max)
        );
    }

    public static IModifier WidthIn(
        this IModifier modifier,
        LayoutLength min = default,
        LayoutLength max = default
    )
    {
        return modifier + new SizeInModifierImpl(
            minWidth: min,
            maxWidth: max,
            minHeight: default,
            maxHeight: default
        );
    }

    public static IModifier HeightIn(
        this IModifier modifier,
        LayoutLength min = default,
        LayoutLength max = default
    )
    {
        return modifier + new SizeInModifierImpl(
            minWidth: default,
            maxWidth: default,
            minHeight: min,
            maxHeight: max
        );
    }
}

internal class SizeInModifierImpl : BaseModifier<SizeInModifierImpl>
{
    private readonly LayoutLength _minWidth;
    private readonly LayoutLength _maxWidth;
    private readonly LayoutLength _minHeight;
    private readonly LayoutLength _maxHeight;

    public SizeInModifierImpl(LayoutLength minWidth, LayoutLength maxWidth, LayoutLength minHeight,
        LayoutLength maxHeight)
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
    }

    public override void Apply(VisualElement element)
    {
        if (_minWidth.HasValue)
            element.style.minWidth = _minWidth;
        if (_maxWidth.HasValue)
            element.style.maxWidth = _maxWidth;
        if (_minHeight.HasValue)
            element.style.minHeight = _minHeight;
        if (_maxHeight.HasValue)
            element.style.maxHeight = _maxHeight;
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