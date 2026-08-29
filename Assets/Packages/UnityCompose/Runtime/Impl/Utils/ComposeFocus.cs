using System;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class VisualElementExtensions
{
    private const string ComposeFocusKey = "UnityCompose_ComposeFocus";
    
    internal static ComposeFocus ComposeFocus(this VisualElement element)
    {
        var focus = element.UserData().GetOrNull(ComposeFocusKey) as ComposeFocus;
        if (focus == null)
        {
            focus = new ComposeFocus();
            element.UserData()[ComposeFocusKey] = focus;
        }

        return focus;
    }
}

internal class ComposeFocus
{
    private FocusState _state;
    private readonly IMutableStableList<Action<FocusState>> _listeners = MutableStableListOf<Action<FocusState>>();

    public void AddListener(Action<FocusState> listener)
    {
        _listeners.Add(listener);
    }
    
    public void RemoveListener(Action<FocusState> listener)
    {
        _listeners.Remove(listener);
    }

    public void Focus()
    {
        if (_state.IsFocused)
            return;
        _state = new FocusState(IsFocused: true);
        foreach (var listener in _listeners)
            listener(_state);
    }

    public void Unfocus()
    {
        if (!_state.IsFocused)
            return;
        _state = new FocusState(IsFocused: false);
        foreach (var listener in _listeners)
            listener(_state);
    }
}