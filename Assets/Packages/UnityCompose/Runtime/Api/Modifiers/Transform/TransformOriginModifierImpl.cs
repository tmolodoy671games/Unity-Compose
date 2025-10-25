// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier TransformOrigin(
        this IModifier modifier,
        float originX = -1,
        float originY = -1,
        float origin = -1
    )
    {
        return modifier + new TransformOriginModifierImpl(
            new Vector2(
                ParamUtils.Resolve(originX, origin),
                ParamUtils.Resolve(originY, origin)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier TransformOrigin(this IModifier modifier, Vector2 origin)
    {
        return modifier + new TransformOriginModifierImpl(origin);
    }
}

internal class TransformOriginModifierImpl : BaseModifier<TransformOriginModifierImpl>
{
    private readonly Vector2 _origin;

    public TransformOriginModifierImpl(Vector2 origin)
    {
        _origin = origin;
    }

    public override void Apply(VisualElement element)
    {
        element.style.transformOrigin = new TransformOrigin(_origin.x, _origin.y);
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
        return _origin == other._origin;
    }
}