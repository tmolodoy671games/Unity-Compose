using System;
using System.Runtime.CompilerServices;
using System.Text;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine;
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

    private IComposeGroup? _invalidationRoot;

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
        var newGroup = new ComposeGroup<int>(ResolvedComposeKey.Create(composeKey, 0), null);
        newGroup.Element = element;
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
        var group = _invalidationRoot != null
            ? (ComposeGroup<TState>)_invalidationRoot
            : parentGroup.GetOrCreateChild<TState>(key);
        ComposeInvalidator.CancelInvalidate(group);
        if (_compositionLocalProviders.IsNotEmpty())
            group.ParentCompositionLocalProvider = _compositionLocalProviders.Peek();
        if (_elements.IsNotEmpty())
            group.ElementIndexInParent = _elements.Peek().CurrentIndex;
        if (_invalidationRoot == null)
            Reinsert(group);

        // Try skipping:
        if (_invalidationRoot == null && group.State.Equals(state))
        {
            if (_elements.IsNotEmpty())
            {
                var elementIndex = _elements.Peek();
                elementIndex.CurrentIndex += group.ElementsCount;
            }

            return true;
        }

        _invalidationRoot = null;
        _groups.Push(group);
        group.State = state;
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
        if (_elements.IsNotEmpty() && currentGroup.Element != null)
        {
            _elements.Peek().CurrentIndex++;
        }

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

    private void Reinsert(IComposeGroup currentGroup)
    {
        if (_elements.IsEmpty())
            return;
        var elementIndex = _elements.Peek();
        var elementsCount = elementIndex.Element.childCount;
        currentGroup.ElementIndexInParent = Math.Clamp(
            currentGroup.ElementIndexInParent,
            0,
            elementsCount > 0 ? elementsCount - 1 : 0
        );
        if (currentGroup.Element != null)
            elementIndex.Element.FastReinsert(elementIndex.CurrentIndex, currentGroup.Element);
        else
        {
            for (var i = 0; i < currentGroup.NestedElements.Count; i++)
            {
                var insertResult =
                    elementIndex.Element.FastReinsert(elementIndex.CurrentIndex + i, currentGroup.NestedElements[i]);
                if (!insertResult)
                    break;
            }
        }
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
        {
            Reinsert(currentGroup);
            _elements.Push(new ComposeElementIndex(cachedElement));
            return cachedElement;
        }

        var newElement = new TElement();
        currentGroup.Element = newElement;
        currentGroup.NestedElements.Clear();
        foreach (var ancestor in currentGroup.Ancestors())
        {
            if (ancestor.Element != null)
                break;
            ancestor.NestedElements.Add(currentGroup.Element);
        }

        Reinsert(currentGroup);
        _elements.Push(new ComposeElementIndex(newElement));
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
        var currentCompositionLocal = _compositionLocalProviders.IsNotEmpty()
            ? _compositionLocalProviders.Peek()
            : null;
        var currentGroup = _groups.Peek();
        var state = Remember(
            key: new ComposeKey(
                FileName: "CompositionLocal",
                MemberName: "CompositionLocal",
                LineNumber: 0
            ),
            compareKey: string.Empty,
            defaultValueFactory: _ =>
            {
                if (currentCompositionLocal != null &&
                    currentCompositionLocal.TryGet(compositionLocal, out var rootState))
                    return rootState;
                var providers = currentGroup.Ancestors(includeSelf: true)
                    .SelectNotNull(static it => it.ParentCompositionLocalProvider);
                foreach (var provider in providers)
                {
                    if (provider.TryGet(compositionLocal, out var providedState))
                        return providedState;
                }

                return null;
            }
        );
        return state?.Value is TValue value ? value : defaultValueFactory();
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
        var parent = composeGroup.Parent;
        if (parent != null)
            _groups.Push(parent);
        var parentElement = FindElement(composeGroup);
        if (parentElement != null)
        {
            var index = new ComposeElementIndex(parentElement)
            {
                CurrentIndex = composeGroup.ElementIndexInParent
            };
            _elements.Push(index);
        }

        if (composeGroup.ParentCompositionLocalProvider != null)
            _compositionLocalProviders.Push(composeGroup.ParentCompositionLocalProvider);
        _invalidationRoot = composeGroup;
        try
        {
            composeGroup.Restart?.Invoke();
            if (composeGroup.ParentCompositionLocalProvider != null)
                _compositionLocalProviders.Pop();
            if (parent != null)
                _groups.Pop();
            if (parentElement != null)
                _elements.Pop();
        }
        finally
        {
            while (_groups.IsNotEmpty())
                _groups.Pop();
            while (_elements.IsNotEmpty())
                _elements.Pop();
            while (_compositionLocalProviders.IsNotEmpty())
                _compositionLocalProviders.Pop();
            ComposeInvalidator.InstantInvalidate();
        }
    }

    private static VisualElement? FindElement(IComposeGroup composeGroup)
    {
        var group = composeGroup.Parent;
        while (group != null && group.Element == null)
        {
            group = group.Parent;
        }

        return group?.Element.NotNull();
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
    public TValue Remember<TValue>(
        Func<TState, TValue> defaultValueFactory,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return CurrentComposer.Remember(
            key: new ComposeKey(
                FileName: filePath,
                MemberName: memberName,
                LineNumber: lineNumber
            ),
            State,
            defaultValueFactory
        );
    }
}