// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Offset(this IModifier modifier, float x, float y)
    {
        return modifier + new OffsetModifierImpl(new Vector2(x, y));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Offset(this IModifier modifier, float offset)
    {
        return modifier + new OffsetModifierImpl(new Vector2(offset, offset));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Offset(this IModifier modifier, Vector2 offset)
    {
        return modifier + new OffsetModifierImpl(offset);
    }
}

internal class OffsetModifierImpl : BaseModifier<OffsetModifierImpl>
{
    private readonly Vector2 _offset;

    public OffsetModifierImpl(Vector2 offset)
    {
        _offset = offset;
    }

    public override void Apply(VisualElement element)
    {
        element.style.translate = new Translate(_offset.x, _offset.y);
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
        return _offset == other._offset;
    }
}