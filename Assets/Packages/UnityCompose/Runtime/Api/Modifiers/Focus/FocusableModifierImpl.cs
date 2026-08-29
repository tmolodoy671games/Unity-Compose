// ReSharper disable CheckNamespace

using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Focusable(
        this IModifier modifier,
        bool enabled = true
    )
    {
        if (!enabled)
            return modifier;
        return modifier + FocusableModifierImpl.Instance;
    }
}

internal class FocusableModifierImpl : BaseModifier<FocusableModifierImpl>
{
    public static readonly FocusableModifierImpl Instance = new();
    
    private FocusableModifierImpl() {}
    
    public override void Apply(VisualElement element)
    {
        element.panel.visualTree.Q<ComposeView>().FocusManager().Add(element);
    }

    public override void Revert(VisualElement element)
    {
        element.panel.visualTree.Q<ComposeView>().FocusManager().Remove(element);
    }

    protected override bool Equals(FocusableModifierImpl other)
    {
        return true;
    }
}