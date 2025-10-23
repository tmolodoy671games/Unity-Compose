// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier TransformOrigin(this IModifier modifier, float origin)
    {
        return modifier + new TransformOriginModifierImpl(Vector2.one * origin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier TransformOrigin(this IModifier modifier, float originX, float originY)
    {
        return modifier + new TransformOriginModifierImpl(new Vector2(originX, originY));
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
        throw new System.NotImplementedException();
    }
}