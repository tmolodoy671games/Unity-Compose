// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Name(this IModifier modifier, string name)
    {
        return modifier + new NameModifierImpl(name);
    }
}

internal class NameModifierImpl : BaseModifier<NameModifierImpl>
{
    private readonly string _name;

    public NameModifierImpl(string name)
    {
        _name = name;
    }

    public override void Apply(VisualElement element)
    {
        element.name = _name;
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        modifiedProperties.Add(ComposeModifiedProperty.Name);
    }

    public override void Revert(VisualElement element)
    {
        element.name = "";
    }

    protected override bool Equals(NameModifierImpl other)
    {
        return _name == other._name;
    }
}