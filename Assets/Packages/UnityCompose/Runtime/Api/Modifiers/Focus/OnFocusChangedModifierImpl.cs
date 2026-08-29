using System;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnFocusChanged(
        this IModifier modifier,
        Action<FocusState> onFocusChanged
    )
    {
        return modifier + new OnFocusChangedModifierImpl(onFocusChanged);
    }
}

public readonly record struct FocusState(
    bool IsFocused
);

internal class OnFocusChangedModifierImpl : BaseModifier<OnFocusChangedModifierImpl>
{
    private readonly Action<FocusInEvent> _onFocusIn;
    private readonly Action<FocusOutEvent> _onFocusOut;
    private readonly Action<FocusState> _onFocusChanged;

    public OnFocusChangedModifierImpl(Action<FocusState> onFocusChanged)
    {
        _onFocusChanged = onFocusChanged;
        _onFocusIn = _ => onFocusChanged(new FocusState(IsFocused: true));
        _onFocusOut = _ => onFocusChanged(new FocusState(IsFocused: false));
    }

    public override void Apply(VisualElement element)
    {
        element.ComposeFocus().AddListener(_onFocusChanged);
        // element.GetComposeCallback<FocusInEvent>().Add(_onFocusIn);
        // element.GetComposeCallback<FocusOutEvent>().Add(_onFocusOut);
    }

    public override void Revert(VisualElement element)
    {
        element.ComposeFocus().AddListener(_onFocusChanged);
        // element.GetComposeCallback<FocusInEvent>().Remove(_onFocusIn);
        // element.GetComposeCallback<FocusOutEvent>().Remove(_onFocusOut);
    }

    protected override bool Equals(OnFocusChangedModifierImpl other)
    {
        return _onFocusChanged == other._onFocusChanged &&
               _onFocusIn == other._onFocusIn &&
               _onFocusOut == other._onFocusOut;
    }
}