using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal record CaptureMouseModifierImpl : UnityModifier
{
    public static readonly CaptureMouseModifierImpl Instance = new();

    private CaptureMouseModifierImpl()
    {
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    ) => element.CaptureMouse();

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    ) => element.ReleaseMouse();
}