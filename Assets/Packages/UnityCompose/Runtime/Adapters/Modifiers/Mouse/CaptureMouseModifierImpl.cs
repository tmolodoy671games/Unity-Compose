using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class CaptureMouseModifierImpl : UnityModifier<CaptureMouseModifierImpl>
{
    public static readonly CaptureMouseModifierImpl Instance = new();

    private CaptureMouseModifierImpl()
    {
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.CaptureMouse();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.ReleaseMouse();
    }

    protected override bool Equals(CaptureMouseModifierImpl other) => true;
    public override int GetHashCode() => 2;
}