#define STRUCT_OPTIMIZATIONS
// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public class Composer
{
    private static Composer? _current;

    public static Composer? Current => _current;

    private readonly SlotTableWriter _writer;

    internal Composer()
    {
        _writer = new SlotTableWriter(this);
    }

    public void SetAsCurrentComposer()
    {
        _current = this;
    }

    public void ResetAsCurrentComposer()
    {
        // if (_current == this)
        //     _current = null;
    }

    #region Restart Group

    public bool ShouldExecute()
    {
        return ShouldExecute(SingletonState.Instance);
    }

    public void StartRestartGroup(int key)
    {
        _writer.StartRestartGroup(key);
    }

    public bool ShouldExecute<T>(T state)
    {
        if (_writer.IsInInvalidationRoot())
            return true;
        var existingState = _writer.GetPreviousState<T>();
        _writer.UpdatePreviousState(state);
        return !existingState.Equals(state);
    }

    public bool ShouldExecuteAsStruct<T>(T state) where T : struct
    {
#if STRUCT_OPTIMIZATIONS
        if (_writer.IsInInvalidationRoot())
            return true;
        var existingState = _writer.GetPreviousStateAsStruct<T>();
        _writer.UpdatePreviousStateAsStruct(state);
        return !existingState.Equals(state);
#else
        return ShouldExecute(state);
#endif
    }

    public void SkipToGroupEnd()
    {
        _writer.SkipToGroupEnd();
    }

    public IScopeUpdateScope? EndRestartGroup(int groupKey)
    {
        var scope = _writer.GetRestartScope();
        _writer.EndRestartGroup(groupKey);
        scope?.SyncFrame();
        return scope;
    }

    #endregion

    #region Replace Group

    public void StartReplaceGroup(int groupKey)
    {
        _writer.StartReplaceGroup(groupKey);
    }

    public void EndReplaceGroup(int groupKey)
    {
        _writer.EndReplaceGroup(groupKey);
    }

    #endregion

    #region Reusable Group

    public void StartReusableGroup(int key)
    {
        _writer.StartReusableGroup(key);
    }

    public void EndReusableGroup(int key)
    {
        _writer.EndReusableGroup(key);
    }

    #endregion

    #region Remember

    public bool Changed()
    {
        return Changed(SingletonState.Instance);
    }

    public bool Changed<TState>(TState state)
    {
        var existingKey = _writer.ReadAndWrite(state);
        return !existingKey.Equals(state);
    }

    public bool ChangedAsStruct<T>(T state) where T : struct
    {
#if STRUCT_OPTIMIZATIONS
        var existingKey = _writer.ReadAndWriteAsStruct(state);
        return !existingKey.Equals(state);
#else
        return Changed(state);
#endif
    }

    public T RememberedValue<T>()
    {
        var result = _writer.Read<T>().Value;
        _writer.IncrementSlotIndex();
        return result;
    }

    public T RememberedValueAsStruct<T>() where T : struct
    {
        var result = _writer.ReadAsStruct<T>().Value;
        _writer.IncrementSlotIndex();
        return result;
    }

    public T UpdateRememberedValue<T>(T update)
    {
        _writer.Write(update);
        return update;
    }

    public T UpdateRememberedValue<T>(Func<T> value) => UpdateRememberedValue(value());

    #endregion

    #region Restarting

    public void Capture(BaseMutableStateImpl state)
    {
        var restartScope = _writer.RequireRestartScope();
        if (restartScope == null)
            return;
        if (state.Add(restartScope) && state.Log)
            Debug.Log($"{state} Capture {restartScope._groupAnchor}");
    }

    public void Reset()
    {
        _writer.ResetTo(0, null);
    }

    public void Clear()
    {
        _writer.Clear();
    }

    #endregion

    #region Visual Element

    public T GetOrCreateVisualElement<T>() where T : VisualElement, new()
    {
        var existingElement = _writer.GetVisualElement<T>();
        if (existingElement != null)
            return existingElement;
        var newElement = new T();
        _writer.WriteVisualElement(newElement);
        return newElement;
    }

    public int GetElementIndex()
    {
        return _writer.GetCurrentElementIndex();
    }

    public void SetVisualElement(VisualElement visualElement)
    {
        _writer.WriteVisualElement(visualElement);
    }

    public void EnterVisualElement()
    {
        _writer.EnterVisualElement();
    }

    #endregion

    #region Composition Local

    public void StartLocalGroup(int groupKey)
    {
        _writer.StartLocalGroup(groupKey);
    }

    public void EndLocalGroup(int groupKey)
    {
        _writer.EndLocalGroup(groupKey);
    }

    public T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory)
    {
        return _writer.GetCompositionLocal(compositionLocal, defaultValueFactory);
    }

    public void UpdateCompositionLocal(IImmutableStableList<CompositionLocalProvides> provides, bool log = false)
    {
        _writer.SetCompositionLocal(provides);
    }

    #endregion

    public override string ToString()
    {
        return _writer.ToString();
    }
}