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
    public static IModifier Offset(
        this IModifier modifier,
        float x = -1,
        float y = -1,
        float offset = -1
    )
    {
        return modifier + new OffsetModifierImpl(
            new Vector2(
                ParamUtils.Resolve(x, offset),
                ParamUtils.Resolve(y, offset)
            )
        );
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