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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier SizeIn(
        this IModifier modifier,
        float min = -1,
        float max = -1,
        float minWidth = -1,
        float maxWidth = -1,
        float minHeight = -1,
        float maxHeight = -1
    )
    {
        return modifier + new SizeInModifierImpl(
            minWidth: ParamUtils.Resolve(minWidth, min),
            maxWidth: ParamUtils.Resolve(maxWidth, max),
            minHeight: ParamUtils.Resolve(minHeight, min),
            maxHeight: ParamUtils.Resolve(maxHeight, max)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier SizeIn(
        this IModifier modifier,
        Optional<Vector2> min = default,
        Optional<Vector2> max = default
    )
    {
        var resolvedMin = min.GetOrDefault(-Vector2.one);
        var resolvedMax = min.GetOrDefault(-Vector2.one);
        return modifier + new SizeInModifierImpl(
            minWidth: resolvedMin.x,
            maxWidth: resolvedMax.x,
            minHeight: resolvedMin.y,
            maxHeight: resolvedMax.y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier WidthIn(
        this IModifier modifier,
        float min = -1,
        float max = -1
    )
    {
        return modifier + new SizeInModifierImpl(
            minWidth: min,
            maxWidth: max,
            minHeight: -1,
            maxHeight: -1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier HeightIn(
        this IModifier modifier,
        float min = -1,
        float max = -1
    )
    {
        return modifier + new SizeInModifierImpl(
            minWidth: -1,
            maxWidth: -1,
            minHeight: min,
            maxHeight: max
        );
    }
}

internal class SizeInModifierImpl : BaseModifier<SizeInModifierImpl>
{
    private readonly float _minWidth;
    private readonly float _maxWidth;
    private readonly float _minHeight;
    private readonly float _maxHeight;

    public SizeInModifierImpl(float minWidth, float maxWidth, float minHeight, float maxHeight)
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
    }

    public override void Apply(VisualElement element)
    {
        if (_minWidth >= 0)
            element.style.minWidth = _minWidth;
        if (_maxWidth >= 0)
            element.style.maxWidth = _maxWidth;
        if (_minHeight >= 0)
            element.style.minHeight = _minHeight;
        if (_maxHeight >= 0)
            element.style.maxHeight = _maxHeight;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        if (_minWidth >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MinWidth);
        if (_maxWidth >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MaxWidth);
        if (_minHeight >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MinHeight);
        if (_maxHeight >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MaxHeight);
    }

    public override void Revert(VisualElement element)
    {
        if (_minWidth >= 0)
            element.style.minWidth = StyleKeyword.Null;
        if (_maxWidth >= 0)
            element.style.maxWidth = StyleKeyword.Null;
        if (_minHeight >= 0)
            element.style.minHeight = StyleKeyword.Null;
        if (_maxHeight >= 0)
            element.style.maxHeight = StyleKeyword.Null;
    }

    protected override bool Equals(SizeInModifierImpl other)
    {
        return _minWidth.Equals(other._minWidth) &&
               _maxWidth.AlmostEquals(other._maxWidth) &&
               _minHeight.AlmostEquals(other._minHeight) &&
               _maxHeight.AlmostEquals(other._maxHeight);
    }
}