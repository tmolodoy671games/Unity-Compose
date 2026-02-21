// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier CapturePointer(this IModifier modifier, bool capture = true, int pointer = 0)
    {
        if (!capture)
            return modifier;
        return modifier + CapturePointerModifierImpl.Instance;
    }
}

internal class CapturePointerModifierImpl : BaseModifier<CapturePointerModifierImpl>
{
    public static readonly CapturePointerModifierImpl Instance = new();

    private CapturePointerModifierImpl()
    {
    }

    public override void Apply(VisualElement element) => element.CapturePointer(0);
    public override void Revert(VisualElement element) => element.ReleasePointer(0);

    protected override bool Equals(CapturePointerModifierImpl other) => true;
}