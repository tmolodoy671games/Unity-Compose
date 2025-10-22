using System;
using System.Text;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeGroup
{
    private class EmptyState
    {
        public static readonly EmptyState Instance = new();

        private EmptyState()
        {
        }
    }

    private static readonly Action EmptyAction = () => { };

    public readonly IMutableStableDictionary<object, ComposeGroupState> Children =
        IMutableStableDictionary.Create<object, ComposeGroupState>();

    public readonly IMutableStableDictionary<object, ComposeInvocationState> Invocations =
        IMutableStableDictionary.Create<object, ComposeInvocationState>();

    public readonly IMutableStableDictionary<object, ComposeRememberState> RememberedValues =
        IMutableStableDictionary.Create<object, ComposeRememberState>();

    public readonly IMutableStableSet<BaseMutableStateImpl> CapturedStates = IMutableStableSet.Create<BaseMutableStateImpl>();
    public CompositionLocal? CompositionLocal;

    public readonly object Key;
    public readonly ComposeGroup? Parent;
    public int IndexInParent = -1;

    public Action Restart = EmptyAction;
    public VisualElement? Element;
    public readonly IMutableStableList<VisualElement> NestedElements = IMutableStableList.Create<VisualElement>();
    public object? State = EmptyState.Instance;
    public int ElementIndex = 0;
    public int ElementsCount = 0;

    public ComposeGroup(object key, ComposeGroup? parent)
    {
        Key = key;
        Parent = parent;
    }

    public override string ToString()
    {
        var builder = new StringBuilder($"{Key}[{IndexInParent}]");
        if (Element != null)
        {
            builder.Append($" {Element.Format()}");
        }

        builder.Append($", ElementIndex={ElementIndex}");
        builder.Append($", ElementsCount={ElementsCount}");

        return builder.ToString();
    }
}