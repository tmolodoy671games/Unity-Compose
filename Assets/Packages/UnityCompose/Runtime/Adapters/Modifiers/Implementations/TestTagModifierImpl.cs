using System;
using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Implementations;

internal class TestTagModifierImpl : UnityModifier<TestTagModifierImpl>
{
    private readonly string _tag;

    public TestTagModifierImpl(string tag)
    {
        _tag = tag;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.name = _tag;
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.name = "";
    }

    protected override bool Equals(TestTagModifierImpl other) => _tag == other._tag;
    public override int GetHashCode() => HashCode.Combine(_tag);
}