using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class ComposeRestartScope : IScopeUpdateScope, IComposeDisposable
{
    private static readonly ObjectPool<ComposeRestartScope> _pool = new(() => new());

    private readonly HashSet<BaseMutableStateImpl> _states = new();
    private SlotTableWriter _writer = null!;
    private AnchorId _groupAnchor;
    private CompositionLocalMap? _compositionLocalMap;
    private VisualElement? _visualElement;
    private bool _isRequestedToRestart;
    private bool _isDisposed;

    private Action? _restartCallback;

    public bool IsDisposed => _isDisposed;

    public static ComposeRestartScope Get(
        AnchorId groupAnchor,
        SlotTableWriter writer,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element
    )
    {
        var instance = ComposeConstants.Pooling ? _pool.Get() : new ComposeRestartScope();
        instance._groupAnchor = groupAnchor;
        instance._isDisposed = false;
        instance._writer = writer;
        instance._compositionLocalMap = compositionLocalMap;
        instance._visualElement = element;
        return instance;
    }

    private ComposeRestartScope()
    {
    }

    public void UpdateScope(Action restartCallback)
    {
        _restartCallback = restartCallback;
    }

    public void Add(BaseMutableStateImpl state) => _states.Add(state);
    public void Remove(BaseMutableStateImpl state) => _states.Remove(state);

    public void RequestRestart()
    {
        if (_isRequestedToRestart)
            return;
        ComposeInvalidator.RequestInvalidate(this);
        _isRequestedToRestart = true;
    }

    public void Restart()
    {
        _writer.RequestCurrentComposer();
        if (_writer.ResetTo(_groupAnchor, _compositionLocalMap, _visualElement))
            _restartCallback?.Invoke();
        _writer.ReleaseCurrentComposer();
        _isRequestedToRestart = false;
    }

    public override string ToString() => "ComposeRestartScope";

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        if (_isRequestedToRestart)
            ComposeInvalidator.CancelInvalidate(this);
        foreach (var state in _states)
            state.Remove(this);
        _states.Clear();
        _isRequestedToRestart = false;
        _pool.Return(this);
    }
}