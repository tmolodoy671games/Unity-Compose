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
    public static IModifier TransformOrigin(
        this IModifier modifier,
        LayoutLength originX = default,
        LayoutLength originY = default,
        Optional<(LayoutLength X, LayoutLength Y)> origin = default
    )
    {
        return modifier + new TransformOriginModifierImpl(
            x: originX.HasValue ? originX : origin.HasValue ? origin.Value.X : default,
            y: originY.HasValue ? originY : origin.HasValue ? origin.Value.Y : default
        );
    }
}

internal class TransformOriginModifierImpl : BaseModifier<TransformOriginModifierImpl>
{
    private readonly LayoutLength _x;
    private readonly LayoutLength _y;

    public TransformOriginModifierImpl(LayoutLength x, LayoutLength y)
    {
        _x = x;
        _y = y;
    }

    public override void Apply(VisualElement element)
    {
        element.style.transformOrigin = new TransformOrigin(_x.ToLength(), _y.ToLength());
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.TransformOrigin);
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