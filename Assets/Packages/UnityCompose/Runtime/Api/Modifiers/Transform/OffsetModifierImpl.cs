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
    public static IModifier Offset(
        this IModifier modifier,
        LayoutLength x = default,
        LayoutLength y = default,
        Optional<(LayoutLength X, LayoutLength Y)> offset = default
    )
    {
        return modifier + new OffsetModifierImpl(
            x: x.HasValue ? x : offset.HasValue ? offset.Value.X : default,
            y: y.HasValue ? y : offset.HasValue ? offset.Value.Y : default
        );
    }
}

internal class OffsetModifierImpl : BaseModifier<OffsetModifierImpl>
{
    private readonly LayoutLength _x;
    private readonly LayoutLength _y;

    public OffsetModifierImpl(LayoutLength x, LayoutLength y)
    {
        _x = x;
        _y = y;
    }

    public override void Apply(VisualElement element)
    {
        element.style.translate = new Translate(_x.ToLength(), _y.ToLength());
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Translate);
    }

    public override void Revert(VisualElement element)
    {
        element.style.translate = StyleKeyword.Null;
    }

    protected override bool Equals(OffsetModifierImpl other)
    {
        return _x.Equals(other._x) && _y.Equals(other._y);
    }
}