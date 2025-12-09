// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public class Composer : IComposer
{
    public static readonly Composer Instance = new();

    private readonly SlotTableWriter _writer = new(new SlotTable());

    #region Restart Group

    public void StartRestartGroup(int key)
    {
        _writer.StartRestartGroup(key);
    }

    public bool ShouldExecute()
    {
        return ShouldExecuteAsStruct(Unit.Instance);
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
        if (_writer.IsInInvalidationRoot())
            return true;
        var existingState = _writer.GetPreviousStateAsStruct<T>();
        _writer.UpdatePreviousStateAsStruct(state);
        return !existingState.Equals(state);
    }

    public void SkipToGroupEnd()
    {
        _writer.SkipToGroupEnd();
    }

    public IScopeUpdateScope? EndRestartGroup(int groupKey)
    {
        var scope = _writer.GetRestartScope();
        _writer.EndRestartGroup(groupKey);
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

    public bool RememberedKeyChanged(int groupKey)
    {
        return RememberedKeyChanged(groupKey, Unit.Instance);
    }

    public bool RememberedKeyChanged<TState>(int groupKey, TState state)
    {
        // _writer.StartReplaceGroup(groupKey);
        var existingKey = _writer.Read<TState>();
        _writer.Write(state);
        _writer.IncrementSlotIndex();
        return !existingKey.Equals(state);
    }

    public bool RememberedKeyChangedAsStruct<T>(int groupKey, T state) where T : struct
    {
        // _writer.StartReplaceGroup(groupKey);
        var existingKey = _writer.ReadAsStruct<T>();
        _writer.WriteAsStruct(state);
        _writer.IncrementSlotIndex();
        return !existingKey.Equals(state);
    }

    public T RememberedValue<T>()
    {
        var result = _writer.Read<T>().Value;
        _writer.IncrementSlotIndex();
        // _writer.EndReplaceGroup();
        return result;
    }

    public T RememberedValueAsStruct<T>() where T : struct
    {
        var result = _writer.ReadAsStruct<T>().Value;
        _writer.IncrementSlotIndex();
        // _writer.EndReplaceGroup();
        return result;
    }

    public T UpdateRememberedValue<T>(T update)
    {
        _writer.Write(update);
        _writer.IncrementSlotIndex();
        // _writer.EndReplaceGroup();
        return update;
    }

    public T UpdateRememberedValueAsStruct<T>(T value) where T : struct
    {
        _writer.WriteAsStruct(value);
        _writer.IncrementSlotIndex();
        // _writer.EndReplaceGroup();
        return value;
    }

    public T UpdateRememberedValue<T>(Func<T> value) => UpdateRememberedValue(value());
    public T UpdateRememberedValueAsStruct<T>(Func<T> value) where T : struct => UpdateRememberedValueAsStruct(value());

    public TValue UpdateLambda<TValue>(TValue value) => UpdateRememberedValue(value);
    public TValue UpdateComposableLambda<TValue>(TValue value) => UpdateRememberedValue(value);

    #endregion

    #region Restarting

    public void Capture(BaseMutableStateImpl state)
    {
        var restartScope = _writer.RequireRestartScope();
        if (restartScope == null)
            return;
        if (state.Add(restartScope) && state.Log)
            Debug.Log($"{state} Capture {_writer.GetGroupIndex(restartScope._groupAnchor)}");
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

    public void UpdateCompositionLocal(IImmutableStableList<CompositionLocalProvides> provides)
    {
        _writer.SetCompositionLocal(provides);
    }

    #endregion

    public override string ToString()
    {
        return _writer.ToString();
    }
}