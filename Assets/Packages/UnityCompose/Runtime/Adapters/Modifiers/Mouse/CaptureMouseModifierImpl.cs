using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Mouse;

internal class CaptureMouseModifierImpl : BaseUnityModifier<CaptureMouseModifierImpl>
{
    public static readonly CaptureMouseModifierImpl Instance = new();
    
    private CaptureMouseModifierImpl() {}
    
    public override void Apply(VisualElement element)
    {
        element.CaptureMouse();
    }

    public override void Revert(VisualElement element)
    {
        element.ReleaseMouse();
    }

    protected override bool Equals(CaptureMouseModifierImpl other) => true;
}