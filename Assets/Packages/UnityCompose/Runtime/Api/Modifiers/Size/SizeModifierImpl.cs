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
    public static IModifier Size(this IModifier modifier, float size = -1, float width = -1, float height = -1)
    {
        return modifier + new SizeModifierImpl(
            width: ParamUtils.Resolve(width, size),
            height: ParamUtils.Resolve(height, size)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Size(this IModifier modifier, Vector2 size)
    {
        return modifier + new SizeModifierImpl(
            width: size.x,
            height: size.y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Width(this IModifier modifier, float width)
    {
        return modifier + new SizeModifierImpl(
            width: width,
            height: -1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Height(this IModifier modifier, float height)
    {
        return modifier + new SizeModifierImpl(
            width: -1,
            height: height
        );
    }
}

internal class SizeModifierImpl : BaseModifier<SizeModifierImpl>
{
    private readonly float _width;
    private readonly float _height;

    public SizeModifierImpl(float width, float height)
    {
        _width = width;
        _height = height;
    }

    public override void Apply(VisualElement element)
    {
        if (_width >= 0)
            element.style.width = _width;
        if (_height >= 0)
            element.style.height = _height;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Width);
        modifiedProperties.Add(ComposeModifiedProperty.Height);
    }

    public override void Revert(VisualElement element)
    {
        if (_width >= 0)
            element.style.width = StyleKeyword.Null;
        if (_height >= 0)
            element.style.height = StyleKeyword.Null;
    }

    protected override bool Equals(SizeModifierImpl other)
    {
        return _width.AlmostEquals(other._width) && _height.AlmostEquals(other._height);
    }
}