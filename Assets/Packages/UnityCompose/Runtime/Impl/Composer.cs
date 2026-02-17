// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public class Composer
{
    public static Composer? Current { get; private set; }

    private readonly SlotTableWriter _writer;

    internal Composer()
    {
        _writer = new SlotTableWriter(this);
    }

    public void SetAsCurrentComposer()
    {
        Current = this;
    }

    public void ResetAsCurrentComposer()
    {
        // if (_current == this)
        //     _current = null;
    }

    #region Restart Group

    public bool IsRestarted() => _writer.IsInInvalidationRoot();

    // public bool ShouldExecute() => ShouldExecute(SingletonState.Instance);

    public bool StartRestartGroup(int key)
    {
        return _writer.StartRestartGroup(key);
    }

    public void SkipToGroupEnd()
    {
        _writer.SkipToGroupEnd();
    }

    public IScopeUpdateScope? EndRestartGroup(int groupKey, bool restarted)
    {
        var scope = _writer.GetRestartScope();
        _writer.EndRestartGroup(groupKey);
        return restarted ? null : scope;
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

    public T WithReplaceGroup<T>(int groupKey, Func<T> factory)
    {
        StartReplaceGroup(groupKey);
        var result = factory();
        EndReplaceGroup(groupKey);
        return result;
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

    #region Key Group

    public void StartMovableGroup<T>(int key, T dataKey) => _writer.StartMovableGroup(key, dataKey);

    public void EndMovableGroup(int key) => _writer.EndMovableGroup(key);

    #endregion

    #region Remember

    public bool Changed()
    {
        return Changed(SingletonState.Instance);
    }

    public bool Changed<TState>(TState state)
    {
        return _writer.ReadAndWrite(state);
    }

    public Optional<T> ReadAsStruct<T>() where T : struct => _writer.ReadAsStruct<T>();

    public bool ChangedAsStruct<T>(T state) where T : struct
    {
        if (ComposeConstants.StructOptimizations)
            return _writer.ReadAndWriteAsStruct(state);
        else
            return Changed(state);
    }

    public T RememberedValue<T>()
    {
        var result = _writer.Read<T>().Value;
        _writer.IncrementSlotIndex();
        return result;
    }

    public T RememberedValueAsStruct<T>() where T : struct
    {
        var result = ComposeConstants.StructOptimizations ? _writer.ReadAsStruct<T>().Value : _writer.Read<T>().Value;
        _writer.IncrementSlotIndex();
        return result;
    }

    public T UpdateRememberedValue<T>(T update)
    {
        _writer.Write(update);
        return update;
    }

    public T UpdateRememberedValue<T>(Func<T> value) => UpdateRememberedValue(value());

    public T UpdateRememberedValueAsStruct<T>(T update) where T : struct
    {
        if (ComposeConstants.StructOptimizations)
            _writer.WriteAsStruct(update);
        else
            _writer.Write(update);

        return update;
    }

    public T UpdateRememberedValueAsStruct<T>(Func<T> update) where T : struct
    {
        var value = update();
        if (ComposeConstants.StructOptimizations)
            _writer.WriteAsStruct(value);
        else
            _writer.Write(value);
        return value;
    }
    
    public void Write<T>(T value) => _writer.Write(value);
    public void WriteAsStruct<T>(T value) where T : struct => _writer.WriteAsStruct(value);

    #endregion

    #region Restarting

    public void Capture(BaseMutableStateImpl state)
    {
        var restartScope = _writer.RequireRestartScope();
        if (restartScope == null)
            return;
        if (state.Add(restartScope) && state.Log)
            Debug.Log($"{state} Capture");
    }

    public void Clear()
    {
        _writer.Clear();
    }

    #endregion

    #region Visual Element

    internal ReusableComposeNode<T> GetReusableNode<T>() where T : VisualElement, new()
    {
        return _writer.GetReusableNode<T>();
    }

    public VisualElement? GetParentVisualElement()
    {
        return _writer.GetParentVisualElement();
    }

    public int GetElementIndex()
    {
        return _writer.GetCurrentElementIndex();
    }

    public void SetVisualElement(VisualElement visualElement)
    {
        _writer.WriteVisualElement(visualElement);
    }

    public void EnterVisualElement(VisualElement element)
    {
        _writer.EnterVisualElement(element);
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

    internal CompositionLocalMap RequireCompositionLocalMap() => _writer.GetCompositionLocalMap().NotNull();

    #endregion

    public ChangedBuilder BuildChanged() => new(this);

    public void Log(object? message) => _writer.Log(message);
    public void LogWarning(object? message) => _writer.LogWarning(message);

    public override string ToString()
    {
        return _writer.ToString();
    }

    public string SlotsToString()
    {
        return _writer.SlotsToString();
    }

    private static bool IsStruct<T>()
    {
        return ComposeConstants.StructOptimizations && typeof(T).IsValueType;
    }
}