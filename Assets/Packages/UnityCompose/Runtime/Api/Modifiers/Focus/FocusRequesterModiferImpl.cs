// ReSharper disable CheckNamespace

using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier FocusRequester(this IModifier modifier, FocusRequester focusRequester)
    {
        return modifier + new FocusRequesterModiferImpl(focusRequester);
    }
}

public class FocusRequester
{
    private VisualElement? _element;
    private FocusManagerImpl? _focusManager;
    private bool _isFocused;

    public void RequestFocus()
    {
        _isFocused = true;
        if (_element == null || _focusManager == null)
            return;
        _focusManager.Focus(_element);
    }

    public void FreeFocus()
    {
        _isFocused = false;
        if (_element == null || _focusManager == null)
            return;
        _focusManager.Unfocus(_element);
    }

    internal void Attach(VisualElement element)
    {
        _element = element;
        _focusManager = element.panel.visualTree.Q<ComposeView>().FocusManager();
        if (_isFocused)
            RequestFocus();
        else
            FreeFocus();
    }

    internal void Detach()
    {
        _element = null;
        _focusManager = null;
    }
}

internal class FocusRequesterModiferImpl : BaseModifier<FocusRequesterModiferImpl>
{
    private readonly FocusRequester _focusRequester;

    public FocusRequesterModiferImpl(FocusRequester focusRequester)
    {
        _focusRequester = focusRequester;
    }

    public override void Apply(VisualElement element)
    {
        _focusRequester.Attach(element);
    }

    public override void Revert(VisualElement element)
    {
        _focusRequester.Detach();
    }

    protected override bool Equals(FocusRequesterModiferImpl other)
    {
        return _focusRequester.Equals(other._focusRequester);
    }
}