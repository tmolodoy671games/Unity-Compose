using System;
using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class Composer
{
    public static readonly Composer Instance = new();

    private SlotTable _table = new();
    private SlotWriter _writer;

    private Composer()
    {
        _writer = new SlotWriter(_table);
    }

    public void Reset()
    {
        var table = new SlotTable();
        _table = table;
        _writer = new SlotWriter(table);
    }

    public bool BeginRootComposeGroup(
        ComposeView element,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var key = ComposeGroupUtils.GetKey(filePath, lineNumber);
        _writer.StartReusableGroup(key, new ComposeUnskippableState(), element);
        return false;
    }

    public bool BeginComposeGroup<TState>(
        TState state,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var key = ComposeGroupUtils.GetKey(filePath, lineNumber);
        RequireCompositionContext();
        _writer.StartReusableGroup(key, state);
        return false;
    }

    public void EndComposeGroup(
        Action restart
    )
    {
        RequireCompositionContext();
        _writer.EndReusableGroup(restart);
    }

    public void EndRootComposeGroup(Action restart)
    {
        RequireCompositionContext();
        // Debug.Log("EndRootComposeGroup()");
        _writer.EndReusableGroup(restart);
        _writer.ResetTo(0);
    }

    internal void BeginCompositionLocal(
        IImmutableStableList<CompositionLocalProvides> provides
    )
    {
        RequireCompositionContext();
        _writer.StartCompositionLocal(provides);
    }

    internal void EndCompositionLocal()
    {
        RequireCompositionContext();
        _writer.EndCompositionLocal();
    }

    public TElement GetOrCreateVisualElement<TElement>() where TElement : VisualElement, new()
    {
        RequireCompositionContext();
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
        RequireCompositionContext();
        return _writer.GetElementIndex();
    }

    #region Remember

    public bool HasRememberedValue<TKey, TValue>(
        TKey key,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        RequireCompositionContext();
        var groupKey = ComposeGroupUtils.GetKey(filePath, lineNumber);
        _writer.StartReplaceableGroup<TKey, TValue>(groupKey);
        var existingValue = _writer.Read<TKey, TValue>();
        var result = existingValue != null && existingValue.Key.Equals(key);
        if (existingValue != null)
            existingValue.Key = key;
        return result;
    }

    public TValue RememberedValue<TKey, TValue>()
    {
        RequireCompositionContext();
        var result = _writer.Read<TKey, TValue>().NotNull().Value;
        _writer.EndReplaceableGroup();
        return result;
    }

    public TValue WriteValue<TKey, TValue>(Func<TValue> value)
    {
        RequireCompositionContext();
        try
        {
            var newValue = value();
            _writer.Write<TKey, TValue>(newValue);
            return newValue;
        }
        finally
        {
            _writer.EndReplaceableGroup();
        }
    }

    #endregion

    public RememberBuilder<TState> WithState<TState>(TState state) => new(state);

    internal TValue GetCompositionLocal<TValue>(
        ICompositionLocal<TValue> compositionLocal,
        Func<TValue> defaultValueFactory
    )
    {
        RequireCompositionContext();
        return _writer.ReadCompositionLocal(compositionLocal, defaultValueFactory);
    }

    internal void Capture(BaseMutableStateImpl state)
    {
        var scope = _writer.GetRestartScope();
        if (scope != null)
            state.Add(scope);
    }

    internal void Invalidate(ComposeGroupRestartScope scope)
    {
        scope.PerformRestart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RequireCompositionContext()
    {
        if (!_writer.IsInCompositionContext)
            throw new IllegalStateException("Not in composition context!");
    }

    public override string ToString()
    {
        return _table.ToString(
            currentGroupIndex: _writer.CurrentGroupIndex,
            parentGroupIndex: _writer.ParentGroupIndex,
            currentSlotIndex: _writer.CurrentSlotIndex
        );
    }

    public void Foo()
    {
        var list = _table.Groups;
        list.Clear();
        var group = new ComposeGroup();
        list.Add(group);
    }
}

// TODO Delete
public readonly record struct RememberBuilder<TState>(TState State)
{
    [Composable]
    public TValue Remember<TValue>(
        Func<TState, TValue> defaultValueFactory
    )
    {
        return defaultValueFactory(default!);
    }
}