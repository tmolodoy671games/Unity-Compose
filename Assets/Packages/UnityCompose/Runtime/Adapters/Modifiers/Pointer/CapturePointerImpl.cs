// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

internal class CapturePointerModifierImpl : BaseUnityModifier<CapturePointerModifierImpl>
{
    private readonly int _pointerId;
    
    public CapturePointerModifierImpl(int pointerId)
    {
        _pointerId = pointerId;
    }

    public override void Apply(VisualElement element) => element.CapturePointer(_pointerId);
    public override void Revert(VisualElement element) => element.ReleasePointer(_pointerId);

    protected override bool Equals(CapturePointerModifierImpl other) => _pointerId == other._pointerId;
}