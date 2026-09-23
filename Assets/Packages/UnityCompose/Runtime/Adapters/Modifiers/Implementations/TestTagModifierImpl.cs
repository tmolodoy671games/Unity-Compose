using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Implementations;

internal class TestTagModifierImpl : BaseUnityModifier<TestTagModifierImpl>
{
    private readonly string _tag;

    public TestTagModifierImpl(string tag)
    {
        _tag = tag;
    }

    public override void Apply(VisualElement element)
    {
        element.name = _tag;
    }

    public override void Revert(VisualElement element)
    {
        element.name = "";
    }

    protected override bool Equals(TestTagModifierImpl other)
    {
        return _tag == other._tag;
    }
}