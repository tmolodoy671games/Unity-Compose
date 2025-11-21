using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class Composer
{
    public static readonly Composer Instance = new();

    private SlotTable _table = new();
    private SlotWriter _writer;

    internal Composer()
    {
        _writer = new SlotWriter(_table);
    }

    public void Reset()
    {
        var table = new SlotTable();
        _table = table;
        _writer = new SlotWriter(table);
    }

    internal SlotTable Table => _table;

    public bool BeginRootComposeGroup(
        VisualElement element,
        [CallerLineNumber] int key = 0
    )
    {
        // Debug.Log("BeginRootComposeGroup()");
        _writer.StartGroup(key);
        return false;
    }

    public bool BeginComposeGroup<TState>(TState state, [CallerLineNumber] int key = 0)
    {
        _writer.StartGroup(key);
        return false;
    }

    public void EndComposeGroup(
        Action restart
    )
    {
        _writer.EndGroup(restart);
    }

    public void EndRootComposeGroup(Action restart)
    {
        // Debug.Log("EndRootComposeGroup()");
        _writer.EndGroup(restart);
        _writer.ResetTo(0);
    }

    internal void BeginCompositionLocal(
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        _writer.WriteCompositionLocal(provides);
    }

    internal TElement GetOrCreateVisualElement<TElement>() where TElement : VisualElement, new()
    {
        var cachedValue = _writer.ReadVisualElement<TElement>();
        if (cachedValue != null)
        {
            _writer.ResetElementIndex();
            return cachedValue;
        }

        var newElement = new TElement();
        _writer.WriteVisualElement(newElement);
        _writer.ResetElementIndex();
        return newElement;
    }

    internal int GetElementIndex()
    {
        return _writer.GetElementIndex();
    }

    internal TValue Remember<TKey, TValue>(TKey key, Func<TValue> defaultValueFactory)
    {
        var existingValue = _writer.Read<TKey, TValue>();
        if (existingValue != null && EqualityUtils.FastEquals(key, existingValue.Key))
        {
            _writer.IncrementSlotIndex();
            return existingValue.Value;
        }

        var newValue = defaultValueFactory();
        _writer.Write(key, newValue);
        _writer.IncrementSlotIndex();
        return newValue;
    }

    internal TValue Remember<TKey, TValue>(TKey key, Func<TKey, TValue> defaultValueFactory)
    {
        var existingValue = _writer.Read<TKey, TValue>();
        if (existingValue != null && EqualityUtils.FastEquals(key, existingValue.Key))
        {
            _writer.IncrementSlotIndex();
            return existingValue.Value;
        }

        var newValue = defaultValueFactory(key);
        _writer.Write(key, newValue);
        _writer.IncrementSlotIndex();
        return newValue;
    }

    public RememberBuilder<TState> WithState<TState>(TState state) => new(state);

    internal TValue GetCompositionLocal<TValue>(
        ICompositionLocal<TValue> compositionLocal,
        Func<TValue> defaultValueFactory
    )
    {
        return _writer.ReadCompositionLocal(compositionLocal, defaultValueFactory);
    }

    internal void Capture(BaseMutableStateImpl state)
    {
        var scope = _writer.GetRestartScope();
        state.Add(scope);
    }

    internal void Invalidate(ComposeGroupRestartScope scope)
    {
        scope.PerformRestart();
    }

    private void RequireCompositionContext()
    {
    }
}

public readonly record struct RememberBuilder<TState>(TState State)
{
    [Composable]
    public TValue Remember<TValue>(
        Func<TState, TValue> defaultValueFactory
    )
    {
        return CurrentComposer.Remember(
            State,
            defaultValueFactory
        );
    }
}