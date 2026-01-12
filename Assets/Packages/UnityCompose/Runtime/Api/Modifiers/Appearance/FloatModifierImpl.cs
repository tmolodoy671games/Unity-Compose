// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Float(this IModifier modifier, bool enabled = true)
    {
        if (!enabled)
            return modifier;
        return modifier + FloatModifierImpl.Instance;
    }
}

internal class FloatModifierImpl : BaseModifier<FloatModifierImpl>
{
    public static readonly FloatModifierImpl Instance = new();

    private FloatModifierImpl()
    {
    }

    public override void Apply(VisualElement element)
    {
        element.style.position = Position.Absolute;
    }

    public override void Revert(VisualElement element)
    {
        element.style.position = StyleKeyword.Null;
    }

    protected override bool Equals(FloatModifierImpl other)
    {
        return true;
    }

    public override string ToString()
    {
        return "Float";
    }
}