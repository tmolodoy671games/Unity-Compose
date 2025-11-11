using System;
using System.Text;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
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

    public readonly IMutableStableDictionary<RememberId, ComposeGroupState> Children =
        IMutableStableDictionary.Create<RememberId, ComposeGroupState>();

    public readonly IMutableStableDictionary<RememberId, ComposeInvocationState> Invocations =
        IMutableStableDictionary.Create<RememberId, ComposeInvocationState>();

    public readonly IMutableStableDictionary<RememberId, ComposeRememberState> RememberedValues =
        IMutableStableDictionary.Create<RememberId, ComposeRememberState>();

    public readonly IMutableStableSet<BaseMutableStateImpl> CapturedStates = IMutableStableSet.Create<BaseMutableStateImpl>();
    public CompositionLocal? CompositionLocal;

    public readonly RememberId Key;
    public readonly ComposeGroup? Parent;
    public int IndexInParent = -1;

    public Action Restart = EmptyAction;
    public VisualElement? Element;
    public readonly IMutableStableList<VisualElement> NestedElements = IMutableStableList.Create<VisualElement>();
    public object? State = EmptyState.Instance;
    public int ElementIndex = 0;
    public int ElementsCount = 0;

    public ComposeGroup(RememberId key, ComposeGroup? parent)
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