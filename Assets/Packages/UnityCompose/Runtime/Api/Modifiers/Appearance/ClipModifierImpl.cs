// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Clip(this IModifier modifier)
    {
        return modifier + ClipModifierImpl.Instance;
    }
}

internal class ClipModifierImpl : BaseModifier<ClipModifierImpl>
{
    public static readonly ClipModifierImpl Instance = new();

    private ClipModifierImpl()
    {
    }

    public override void Apply(VisualElement element)
    {
        element.style.overflow = Overflow.Hidden;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Overflow);
    }

    public override void Revert(VisualElement element)
    {
        element.style.overflow = StyleKeyword.Null;
    }

    protected override bool Equals(ClipModifierImpl other)
    {
        return true;
    }
}