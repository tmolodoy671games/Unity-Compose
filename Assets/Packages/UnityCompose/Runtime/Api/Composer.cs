using System;
using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class Composer
{
    public static readonly Composer Instance = new();

    private readonly IMutableStableStack<IComposeGroup> _groups = IMutableStableStack.Create<IComposeGroup>();

    private readonly IMutableStableStack<ComposeElementIndex> _elements =
        IMutableStableStack.Create<ComposeElementIndex>();

    private readonly IMutableStableStack<ICompositionLocalProvider> _compositionLocalProviders =
        IMutableStableStack.Create<ICompositionLocalProvider>();

    public bool BeginRootComposeGroup(
        VisualElement element,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        if (element.userData is IComposeGroup cachedGroup)
        {
            _groups.Push(cachedGroup);
            _elements.Push(new ComposeElementIndex(element));
            return false;
        }

        var composeKey = new ComposeKey(filePath, memberName, lineNumber);
        var newGroup = new ComposeGroup<int>(ResolvedComposeKey.Create(composeKey, 0), null, 0);
        element.userData = newGroup;
        _groups.Push(newGroup);
        _elements.Push(new ComposeElementIndex(element));
        return false;
    }

    internal bool BeginComposeGroup<TState>(
        TState state,
        ComposeKey key
    )
    {
        RequireCompositionContext();
        var parentGroup = _groups.Peek();
        var group = parentGroup.GetOrCreateChild(key, state);
        if (_compositionLocalProviders.IsNotEmpty())
            group.ParentCompositionLocalProvider = _compositionLocalProviders.Peek();

        // Try skipping:
        if (EqualityUtils.FastEquals(group.State, state))
        {
            if (_elements.IsNotEmpty())
            {
                var elementIndex = _elements.Peek();
                elementIndex.CurrentIndex += group.ElementsCount;
            }

            return true;
        }

        _groups.Push(group);
        if (group.Element != null)
            _elements.Push(new ComposeElementIndex(group.Element));
        return false;
    }

    public bool BeginComposeGroup<TState>(
        TState state,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return BeginComposeGroup(state, new ComposeKey(filePath, memberName, lineNumber));
    }

    public void EndComposeGroup(Action restart)
    {
        RequireCompositionContext();
        var currentGroup = _groups.Pop();
        currentGroup.Restart = restart;
        if (currentGroup.Element != null)
            _elements.Pop();
        if (_elements.IsNotEmpty() && currentGroup.ElementsCount > 0)
            _elements.Peek().CurrentIndex += currentGroup.ElementsCount;
        currentGroup.Reset();
    }

    internal void BeginCompositionLocal(
        ICompositionLocalProvider provider,
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        provider.Update(provides);
        _compositionLocalProviders.Push(provider);
    }

    internal void EndCompositionLocal()
    {
        _compositionLocalProviders.Pop();
    }

    internal TElement GetOrCreateVisualElement<TElement>() where TElement : VisualElement, new()
    {
        RequireCompositionContext();
        var currentGroup = _groups.Peek();
        if (currentGroup.Element is TElement cachedElement)
            return cachedElement;
        var newElement = new TElement();
        currentGroup.Element = newElement;
        currentGroup.NestedElements.Clear();
        foreach (var ancestor in currentGroup.Ancestors())
        {
            if (ancestor.Element != null)
                break;
            ancestor.NestedElements.Add(currentGroup.Element);
        }

        if (_elements.IsNotEmpty())
        {
            var elementIndex = _elements.Peek();
            elementIndex.Element.Insert(elementIndex.CurrentIndex++, newElement);
        }

        return newElement;
    }

    internal TValue Remember<TKey, TValue>(ComposeKey key, TKey compareKey, Func<TKey, TValue> defaultValueFactory)
    {
        RequireCompositionContext();
        var currentGroup = _groups.Peek();
        return currentGroup.Remember(key, compareKey, defaultValueFactory);
    }

    public RememberBuilder<TState> WithState<TState>(TState state) => new(state);

    internal TValue GetCompositionLocal<TValue>(
        ICompositionLocal<TValue> compositionLocal,
        Func<TValue> defaultValueFactory
    )
    {
        RequireCompositionContext();
        var currentGroup = _groups.Peek();
        var currentProvider = currentGroup.ParentCompositionLocalProvider;
        if (currentProvider == null)
            return defaultValueFactory();
        return currentProvider.Get(compositionLocal, defaultValueFactory);
    }

    internal void Capture(BaseMutableStateImpl state)
    {
        if (_groups.IsEmpty())
            return;
        var currentGroup = _groups.Peek();
        state.Add(currentGroup);
    }

    internal void Invalidate(IComposeGroup composeGroup)
    {
        // BRUH
    }

    private void RequireCompositionContext()
    {
        if (_groups.IsEmpty())
            throw new IllegalStateException("Not in composition context!");
    }
}

public readonly record struct RememberBuilder<TState>(TState State)
{
    [Composable]
    public TValue Remember<TValue>(Func<TState, TValue> defaultValueFactory)
    {
        return ComposeFunctions.Remember(State, defaultValueFactory);
    }
}