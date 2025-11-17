using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface IComposeGroup : IDisposable
{
    IComposeGroup? Parent { get; }
    ResolvedComposeKey Key { get; }
    VisualElement? Element { get; set; }
    int ElementsCount { get; }
    IMutableStableCollection<VisualElement> NestedElements { get; }
    Action? Restart { get; set; }
    bool CalledThisStep { get; set; }
    ICompositionLocalProvider? ParentCompositionLocalProvider { get; set; }

    ComposeGroup<TChild> GetOrCreateChild<TChild>(ComposeKey key, TChild state);
    TValue Remember<TKey, TValue>(ComposeKey key, TKey compareKey, Func<TKey, TValue> defaultValueFactory);
    void Reset();
}

internal class ComposeGroup<T> : IComposeGroup
{
    private readonly IRememberStorage _rememberStorage = new RememberStorage();
    
    private readonly IMutableStableDictionary<ResolvedComposeKey, IComposeGroup> _children =
        IMutableStableDictionary.Create<ResolvedComposeKey, IComposeGroup>();

    private readonly IInvocationState _groupInvocationState = new InvocationState();

    public ComposeGroup(ResolvedComposeKey key, IComposeGroup? parent, T initialState)
    {
        Key = key;
        Parent = parent;
        State = initialState;
    }

    public T State { get; }
    public ResolvedComposeKey Key { get; }
    public VisualElement? Element { get; set; }
    public Action? Restart { get; set; }
    public IComposeGroup? Parent { get; }

    public int ElementsCount => Element != null ? 1 : NestedElements.Count;
    public IMutableStableCollection<VisualElement> NestedElements { get; } = IMutableStableSet.Create<VisualElement>();

    public bool CalledThisStep { get; set; }
    public ICompositionLocalProvider? ParentCompositionLocalProvider { get; set; }

    public ComposeGroup<TChild> GetOrCreateChild<TChild>(ComposeKey key, TChild state)
    {
        var resolvedKey = _groupInvocationState.ResolveKey(key);
        if (_children.TryGet(resolvedKey, out var cachedChild))
        {
            if (cachedChild is ComposeGroup<TChild> castedChild)
            {
                castedChild.CalledThisStep = true;
                return castedChild;
            }

            cachedChild.Dispose();
        }

        var result = new ComposeGroup<TChild>(resolvedKey, this, state);
        result.CalledThisStep = true;
        _children[resolvedKey] = result;
        return result;
    }

    public TValue Remember<TKey, TValue>(ComposeKey key, TKey compareKey, Func<TKey, TValue> defaultValueFactory)
    {
        return _rememberStorage.Get(key, compareKey, defaultValueFactory);
    }

    public void Reset()
    {
        foreach (var child in _children.Values.ToImmutableStableList())
        {
            if (!child.CalledThisStep)
            {
                child.Dispose();
                _children.Remove(child.Key);
            }
            else
                child.CalledThisStep = false;
        }

        CalledThisStep = false;
        _groupInvocationState.Reset();
        _rememberStorage.Reset();
    }

    public void Dispose()
    {
        if (Element != null)
        {
            foreach (var ancestor in this.Ancestors())
            {
                if (ancestor.Element != null)
                    break;
                ancestor.NestedElements.Remove(Element);
            }

            Element.parent?.Remove(Element);
        }
        _rememberStorage.Dispose();

        foreach (var child in _children.Values)
            child.Dispose();
    }
}