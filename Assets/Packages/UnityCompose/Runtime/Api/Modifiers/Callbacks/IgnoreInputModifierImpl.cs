// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier IgnoreInput(this IModifier modifier, bool ignore = true)
    {
        if (!ignore)
            return modifier;
        return modifier + IgnoreInputModifierImpl.Instance;
    }
}

internal class IgnoreInputModifierImpl : BaseModifier<IgnoreInputModifierImpl>
{
    public static readonly IgnoreInputModifierImpl Instance = new();

    private IgnoreInputModifierImpl()
    {
    }

    public override void Apply(VisualElement element) => element.pickingMode = PickingMode.Ignore;
    public override void Revert(VisualElement element) => element.pickingMode = PickingMode.Position;
    protected override bool Equals(IgnoreInputModifierImpl other) => true;
    public override string ToString() => "IgnoreInput";
}