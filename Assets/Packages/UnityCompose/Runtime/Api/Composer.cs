// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using Packages.UnityCompose.Impl.Composition.Utils;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Core;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityEngine.UIElements;

namespace UnityCompose;

public class Composer
{
    private readonly SlotWriter _writer = new();

    public static readonly Composer CurrentComposer = new();

    private Composer()
    {
    }

    public void Reset()
    {
        _writer.Clear();
    }

    #region Root Group

    public bool BeginRootComposeGroup(VisualElement root)
    {
        _writer.StartRootGroup(root);
        return false;
    }

    public void EndRootComposeGroup(Action restart)
    {
        RequireCompositionContext();
        _writer.EndRootGroup(restart);
        _writer.ResetToRoot();
    }

    #endregion

    #region Compose Groups

    public bool BeginComposeGroup<T>(
        T state,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        RequireCompositionContext();
        var groupKey = ComposeGroupKey.Get(filePath, lineNumber);
        _writer.StartReusableGroup(groupKey, state);
        return false;
    }

    public void EndComposeGroup(Action restart)
    {
        RequireCompositionContext();
        _writer.EndReusableGroup(restart);
    }

    public T GetOrCreateVisualElement<T>() where T : VisualElement, new()
    {
        RequireCompositionContext();
        var existingElement = _writer.GetVisualElement();
        if (existingElement == null)
        {
            existingElement = new T();
            _writer.SetVisualElement(existingElement);
        }

        return existingElement.CastTo<T>();
    }

    public int GetElementIndex()
    {
        return _writer.GetElementIndex();
    }

    #endregion

    #region Remember

    public bool HasRememberedValue<TKey, TValue>(
        TKey key,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        RequireCompositionContext();
        var groupKey = ComposeGroupKey.Get(filePath, lineNumber);
        _writer.StartReplaceableGroup<TKey, TValue>(groupKey);
        var storedKey = _writer.ReadAndSetKey<TKey, TValue>(key);
        return storedKey.Equals(key);
    }

    public TValue RememberedValue<TKey, TValue>()
    {
        RequireCompositionContext();
        var storedValue = _writer.ReadValue<TKey, TValue>();
        return storedValue;
    }

    public TValue WriteValue<TKey, TValue>(Func<TValue> value)
    {
        RequireCompositionContext();
        try
        {
            var newValue = value();
            _writer.Write<TKey, TValue>(value());
            return newValue;
        }
        finally
        {
            _writer.EndReplaceableGroup();
        }
    }

    #endregion

    #region CompositionLocal

    internal void BeginCompositionLocal(IImmutableStableList<CompositionLocalProvides> provides)
    {
        RequireCompositionContext();
        _writer.UpdateCompositionLocal(provides);
    }

    internal T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        RequireCompositionContext();
        var cachedValue = _writer.GetCompositionLocal(compositionLocal);
        return cachedValue.HasValue ? cachedValue.Value : defaultValueFactory();
    }

    #endregion

    #region Invalidation

    internal void Capture(BaseMutableStateImpl state)
    {
        var scope = _writer.GetRestartScope();
        if (scope != null)
            state.Add(scope);
    }

    internal void Invalidate(ReusableComposeGroup scope)
    {
        scope.PerformRestart();
    }

    #endregion

    public ComposeRememberBuilder<T> WithState<T>(T state) => new(state);

    private void RequireCompositionContext()
    {
        if (!_writer.IsInCompositionContext())
            throw new IllegalStateException("Not in composition context!");
    }

    public override string ToString()
    {
        return _writer.ToString();
    }
}

public record ComposeRememberBuilder<T>(T State)
{
    public TValue Remember<TValue>(Func<T, TValue> defaultValueFactory) => defaultValueFactory(State);
}