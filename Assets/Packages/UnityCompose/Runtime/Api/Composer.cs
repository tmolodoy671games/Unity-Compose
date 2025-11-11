using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class Composer
{
    public static readonly Composer Instance = new();

    private ComposeGroup? _invalidationRoot;
    private readonly Stack<ComposeGroupIndex> _groups = new();
    private readonly Stack<ComposeElementIndex> _elements = new();
    private readonly Stack<CompositionLocal> _compositionLocals = new();

    public bool BeginRootComposeGroup(VisualElement element)
    {
        if (element.userData is ComposeGroup cachedGroup)
        {
            _groups.Push(new ComposeGroupIndex(cachedGroup));
            _elements.Push(new(element));
            return false;
        }

        var group = new ComposeGroup(new RememberId("Root", 34), null)
        {
            Element = element
        };
        _groups.Push(new ComposeGroupIndex(group));
        _elements.Push(new(element));
        element.userData = group;
        return false;
    }

    internal bool BeginComposeGroup(object? state, RememberId key)
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        var groupEntry = _groups.Peek();
        var currentGroup = groupEntry.Group;
        if (_invalidationRoot != null)
        {
            if (currentGroup.CompositionLocal != null)
                _compositionLocals.Push(currentGroup.CompositionLocal);
            _invalidationRoot = null;
            return false;
        }

        var resolvedKey = currentGroup.ResolveKey(key);
        var group = currentGroup.GetOrCreateSubGroup(resolvedKey);
        if (group.IndexInParent >= 0 && group.IndexInParent != groupEntry.Index && _elements.IsNotEmpty())
        {
            var lastElement = _elements.Peek();
            if (group.Element != null)
            {
                lastElement.Element.FastReinsert(lastElement.Index, group.Element);
            }
            else if (group.NestedElements.IsNotEmpty())
            {
                foreach (var element in group.NestedElements.AsEnumerable().Reverse())
                {
                    if (!lastElement.Element.FastReinsert(lastElement.Index, element))
                        break;
                }
            }
        }

        group.IndexInParent = groupEntry.Index;
        groupEntry.Index++;
        if (_compositionLocals.IsNotEmpty())
            group.CompositionLocal = _compositionLocals.Peek();
        if (_elements.IsNotEmpty())
            group.ElementIndex = _elements.Peek().Index;

        if (Equals(group.State, state))
        {
            if (_elements.IsNotEmpty())
            {
                // Insert
                var entry = _elements.Peek();
                var element = group.Element;
                if (element != null && entry.Element.GetOrNull(entry.Index) != element)
                {
                    element.RemoveFromHierarchy();
                    entry.Element.Insert(entry.Index, element);
                }

                entry.Index += group.ElementsCount;
            }

            return true;
        }

        group.State = state;
        _groups.Push(new ComposeGroupIndex(group));
        return false;
    }

    public bool BeginComposeGroup(
        object? state,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int key = 0
    )
    {
        return BeginComposeGroup(state, new RememberId(filePath, key));
    }

    public void EndComposeGroup(Action restart)
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        var currentGroup = _groups.Peek().Group;
        currentGroup.Restart = restart;
        if (currentGroup.Children.Count > 0)
        {
            foreach (var entry in currentGroup.Children.ToList())
            {
                if (!entry.Value.InvokedThisStep)
                    currentGroup.RemoveSubGroup(entry.Value.ComposeGroup);
                else
                    entry.Value.InvokedThisStep = false;
            }
        }

        if (currentGroup.RememberedValues.Count > 0)
        {
            foreach (var entry in currentGroup.RememberedValues.ToList())
            {
                if (!entry.Value.InvokedThisStep)
                {
                    if (entry.Value.Value is IDisposable disposable)
                        disposable.Dispose();
                    currentGroup.RememberedValues.Remove(entry.Key);
                }
                else
                    entry.Value.InvokedThisStep = false;
            }
        }

        if (currentGroup.Invocations.Count > 0)
        {
            foreach (var entry in currentGroup.Invocations.Values)
            {
                entry.InvocationCount = 0;
            }
        }

        if (currentGroup.Element != null)
        {
            _elements.Pop().Index = 0;
        }

        if (_elements.IsNotEmpty())
            currentGroup.ElementsCount = _elements.Peek().Index - currentGroup.ElementIndex;

        _groups.Pop();
        if (_groups.IsEmpty())
            ComposeInvalidator.InstantInvalidate();
    }

    internal T GetOrCreateVisualElement<T>() where T : VisualElement, new()
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        var element = GetOrCreateVisualElementImpl<T>();

        if (_elements.Count > 0)
        {
            // Insert
            var entry = _elements.Peek();
            entry.Element.FastReinsert(entry.Index, element);
            // Increment
            entry.Index++;
        }

        // Push
        _elements.Push(new ComposeElementIndex(element));

        return element;
    }

    internal void BeginCompositionLocal(
        CompositionLocal compositionLocal,
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        if (_compositionLocals.IsNotEmpty())
            compositionLocal.Parent = _compositionLocals.Peek();
        _compositionLocals.Push(compositionLocal);
        var providedKeys = provides.Select(it => it.CompositionLocal).ToImmutableStableSet();
        foreach (var existingEntry in compositionLocal.Provides.ToImmutableStableList())
        {
            if (providedKeys.Contains(existingEntry.Key))
                continue;
            if (existingEntry.Value.Value is IDisposable disposable)
                disposable.Dispose();
            compositionLocal.Provides.Remove(existingEntry.Key);
        }

        foreach (var newEntry in provides)
        {
            if (compositionLocal.Provides.TryGet(newEntry.CompositionLocal, out var cachedProperty))
                cachedProperty.Value = newEntry.Value;
            else
            {
                compositionLocal.Provides[newEntry.CompositionLocal] =
                    new MutableStateImpl<object?>(newEntry.Value, true);
            }
        }
    }

    internal void EndCompositionLocal()
    {
        _compositionLocals.Pop();
    }

    internal T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        var property = FindCompositionLocalProperty(compositionLocal);
        if (property != null)
            return (T)property.Value!;
        return defaultValueFactory();
    }

    private IMutableState<object?>? FindCompositionLocalProperty(ICompositionLocal compositionLocal)
    {
        if (_compositionLocals.IsEmpty()) return null;
        var currentCompositionLocal = _compositionLocals.Peek();
        return Remember(
            new RememberId("CompositionLocal", 232, compositionLocal),
            0,
            () =>
            {
                while (currentCompositionLocal != null)
                {
                    if (currentCompositionLocal.Provides.TryGet(compositionLocal, out var cachedProperty))
                        return cachedProperty;

                    currentCompositionLocal = currentCompositionLocal.Parent;
                }

                return null;
            }
        );
    }

    private T GetOrCreateVisualElementImpl<T>() where T : VisualElement, new()
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        var currentGroup = _groups.Peek().Group;
        var existingElement = currentGroup.Element;
        if (existingElement is T element)
            return element;
        var newElement = new T();
        ComposeGroup? currentParent;
        if (currentGroup.Element != null)
        {
            currentGroup.Element.RemoveFromHierarchy();
            currentParent = currentGroup.Parent;
            while (currentParent is { Element: null })
            {
                currentParent.NestedElements.Remove(currentGroup.Element);
                currentParent = currentParent.Parent;
            }
        }

        currentGroup.Element = newElement;
        currentGroup.NestedElements.Clear();
        currentParent = currentGroup.Parent;
        while (currentParent is { Element: null })
        {
            currentParent.NestedElements.Add(newElement);
            currentParent = currentParent.Parent;
        }

        return newElement;
    }

    internal T Remember<T>(RememberId id, object? key, Func<T> defaultValueFactory)
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        var currentGroup = _groups.Peek().Group;
        var rememberKey = currentGroup.ResolveKey(id);
        key ??= new Optional<object?>(key);
        var isCached = currentGroup.RememberedValues.TryGet(rememberKey, out var cachedValue) &&
                       // cachedValue?.Value is T &&
                       Equals(cachedValue.Key, key);
        if (isCached)
        {
            cachedValue.InvokedThisStep = true;
            return (T)cachedValue.Value!;
        }

        var value = defaultValueFactory();
        if (cachedValue != null!)
        {
            if (cachedValue.Value is IDisposable disposable)
                disposable.Dispose();
            cachedValue.Key = key;
            cachedValue.Value = value;
            cachedValue.InvokedThisStep = true;
        }
        else
            currentGroup.RememberedValues[rememberKey] = new ComposeRememberState(key, value);

        return value;
    }

    internal void LaunchedEffect(RememberId id, object? key, IEnumerator coroutine)
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        Remember(id, key, () => ComposeInvalidator.StartCoroutineAsDisposable(coroutine));
    }

    internal void LaunchedEffect(RememberId id, object? key, Action body)
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        Remember<object?>(id, key, () =>
        {
            body();
            return null;
        });
    }

    internal void DisposableEffect(RememberId id, object? key, Func<IDisposable> disposable)
    {
        if (_groups.IsEmpty()) throw new ArgumentException("Not in composition context!");
        Remember(id, key, disposable);
    }

    internal void Capture(BaseMutableStateImpl mutableState)
    {
        if (_groups.IsEmpty()) return;
        var currentGroup = _groups.Peek().Group;
        if (!currentGroup.CapturedStates.Add(mutableState))
            return;
        mutableState.Add(currentGroup);
    }

    internal void Invalidate(ComposeGroup group)
    {
        _groups.Push(new ComposeGroupIndex(group));
        _invalidationRoot = group;
        group.Restart();
        _groups.Clear();
        _elements.Clear();
        _compositionLocals.Clear();
    }

    internal static string FormatTreeStructure(ComposeGroup root)
    {
        var builder = new StringBuilder();
        FormatTreeStructureRecursive(builder, "", root);
        return builder.ToString();
    }

    private static void FormatTreeStructureRecursive(StringBuilder builder, string indent, ComposeGroup group)
    {
        builder.AppendLine(indent + group);
        foreach (var child in group.Children.OrderBy(it => it.Value.ComposeGroup.IndexInParent))
            FormatTreeStructureRecursive(builder, indent + "  ", child.Value.ComposeGroup);
    }
}