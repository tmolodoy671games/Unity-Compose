using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal interface IComposeGroupDeprecated : IDisposable
{
    IComposeGroupDeprecated? Parent { get; }
    ResolvedComposeKey Key { get; }
    VisualElement? Element { get; set; }
    int ElementsCount { get; }
    IEnumerable<IComposeGroupDeprecated> Children { get; }
    IMutableStableList<VisualElement> NestedElements { get; }
    Action? Restart { get; set; }
    bool CalledThisStep { get; set; }
    ICompositionLocalProvider? ParentCompositionLocalProvider { get; set; }
    int ElementIndexInParent { get; set; }

    ComposeGroupDeprecated<TChild> GetOrCreateChild<TChild>(ComposeKey key);
    TValue Remember<TKey, TValue>(ComposeKey key, TKey compareKey, Func<TKey, TValue> defaultValueFactory);
    void Reset();

    string ToString(bool recursive);
}

internal class ComposeGroupDeprecated<T> : IComposeGroupDeprecated
{
    private readonly IRememberStorage _rememberStorage = new RememberStorage();
    
    private readonly IMutableStableDictionary<ResolvedComposeKey, IComposeGroupDeprecated> _children =
        IMutableStableDictionary.Create<ResolvedComposeKey, IComposeGroupDeprecated>();

    private readonly IInvocationState _groupInvocationState = new InvocationState();

    public ComposeGroupDeprecated(ResolvedComposeKey key, IComposeGroupDeprecated? parent)
    {
        Key = key;
        Parent = parent;
    }

    public Optional<T> State { get; set; }
    public ResolvedComposeKey Key { get; }
    public VisualElement? Element { get; set; }
    public Action? Restart { get; set; }
    public IComposeGroupDeprecated? Parent { get; }

    public int ElementsCount => Element != null ? 1 : NestedElements.Count;
    public IEnumerable<IComposeGroupDeprecated> Children => _children.Values;
    public IMutableStableList<VisualElement> NestedElements { get; } = IMutableStableList.Create<VisualElement>();

    public bool CalledThisStep { get; set; }
    public ICompositionLocalProvider? ParentCompositionLocalProvider { get; set; }

    public int ElementIndexInParent { get; set; }

    public ComposeGroupDeprecated<TChild> GetOrCreateChild<TChild>(ComposeKey key)
    {
        var resolvedKey = _groupInvocationState.ResolveKey(key);
        if (_children.TryGet(resolvedKey, out var cachedChild))
        {
            if (cachedChild is ComposeGroupDeprecated<TChild> castedChild)
            {
                castedChild.CalledThisStep = true;
                return castedChild;
            }

            cachedChild.Dispose();
        }

        var result = new ComposeGroupDeprecated<TChild>(resolvedKey, this);
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
        var initialChildrenCount = _children.Count;
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
        var newChildrenCount = _children.Count;
        if (newChildrenCount < initialChildrenCount)
            UpdateChildIndices();

        _groupInvocationState.Reset();
        _rememberStorage.Reset();
    }

    private void UpdateChildIndices()
    {
        foreach (var child in _children.Values)
        {
            var element = child.Element ?? child.NestedElements.FirstOrDefault();
            if (element == null)
                continue;
            child.ElementIndexInParent = element.parent.IndexOf(element);
        }
    }

    public string ToString(bool recursive)
    {
        if (!recursive)
            return ToString();
        var builder = new StringBuilder();
        ToStringRecursive(builder, "", this);
        return builder.ToString();
    }

    private static void ToStringRecursive(StringBuilder builder, string indent, IComposeGroupDeprecated groupDeprecated)
    {
        builder.Append(indent + groupDeprecated + "\n");
        foreach (var child in groupDeprecated.Children)
            ToStringRecursive(builder, indent + '\t', child);
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

    public override string ToString()
    {
        return $"ComposeGroup(Key={Key}, Element={Element?.GetType().Name})";
    }
}