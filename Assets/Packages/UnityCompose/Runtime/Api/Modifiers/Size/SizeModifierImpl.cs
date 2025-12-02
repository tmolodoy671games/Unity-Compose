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
    public static IModifier Size(
        this IModifier modifier,
        LayoutLength size = default,
        LayoutLength width = default,
        LayoutLength height = default
    )
    {
        return modifier + new SizeModifierImpl(
            width: ParamUtils.Resolve(width, size),
            height: ParamUtils.Resolve(height, size)
        );
    }
    
    public static IModifier Width(this IModifier modifier, LayoutLength width)
    {
        return modifier + new SizeModifierImpl(
            width: width,
            height: default
        );
    }

    public static IModifier Height(this IModifier modifier, LayoutLength height)
    {
        return modifier + new SizeModifierImpl(
            width: default,
            height: height
        );
    }
}

internal class SizeModifierImpl : BaseModifier<SizeModifierImpl>
{
    private readonly LayoutLength _width;
    private readonly LayoutLength _height;

    public SizeModifierImpl(LayoutLength width, LayoutLength height)
    {
        _width = width;
        _height = height;
    }

    public override void Apply(VisualElement element)
    {
        if (_width.HasValue)
            element.style.width = _width;
        if (_height.HasValue)
            element.style.height = _height;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Width);
        modifiedProperties.Add(ComposeModifiedProperty.Height);
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