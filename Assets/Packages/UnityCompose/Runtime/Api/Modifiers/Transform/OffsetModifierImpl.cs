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
        Optional<float> x = default,
        Optional<float> y = default,
        Optional<Vector2> offset = default
    )
    {
        return modifier + new OffsetModifierImpl(
            new Vector2(
                x.GetOrDefault(offset.HasValue ? offset.Value.x : 0f),
                y.GetOrDefault(offset.HasValue ? offset.Value.y : 0f)
            )
        );
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