using System;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdyComposeRestartScope : IComposeRestartScope, IComposeDisposable
{
    private static readonly ObjectPool<SturdyComposeRestartScope> Pool = new(
        factory: () => new SturdyComposeRestartScope(),
        onInit: it => it._isDisposed = false
    );

    public static SturdyComposeRestartScope Get(
        SturdyComposeGroup group,
        SturdySlotTableWriterImpl writer,
        VisualElement? visualElement,
        SturdyComposeGroup? localGroup
    )
    {
        var result = Pool.Get();
        result.Group = group;
        result.VisualElement = visualElement;
        result.Writer = writer;
        result.AncestorLocalGroup = localGroup;
        return result;
    }

    public static void Notify(
        VisualElement parent,
        int startIndex,
        int offset
    )
    {
        if (offset == 0 || parent.childCount <= startIndex)
            return;
    }

    private bool _isDisposed;
    private IMutableStableList<BaseMutableStateImpl>? _capturedStates;
    private bool _isRequestedToRestart;

    public SturdyComposeGroup? Group { get; private set; }
    public VisualElement? VisualElement { get; private set; }
    private Action? RestartCallback { get; set; }
    public SturdySlotTableWriterImpl? Writer { get; private set; }
    public SturdyComposeGroup? AncestorLocalGroup { get; private set; }

    private IMutableStableList<BaseMutableStateImpl> CapturedStates
    {
        get
        {
            if (_capturedStates == null)
                _capturedStates = MutableStableListOf<BaseMutableStateImpl>();
            return _capturedStates;
        }
    }

    private SturdyComposeRestartScope()
    {
    }

    public void UpdateScope(Action restartCallback)
    {
        AssertNotDisposed();
        RestartCallback = restartCallback;
    }

    public void Add(BaseMutableStateImpl state)
    {
        AssertNotDisposed();
        CapturedStates.Add(state);
    }

    public bool RequestRestart()
    {
        if (_isRequestedToRestart)
            return true;
        if (_isDisposed)
            return false;
        AssertNotDisposed();
        ComposeInvalidator.RequestInvalidate(this);
        return true;
    }

    public void Restart()
    {
        _isRequestedToRestart = false;
        if (_isDisposed)
            return;
        if (Group == null || VisualElement == null || RestartCallback == null || Writer == null)
            return;
        Writer.ResetTo(this);
        RestartCallback();
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        if (_isRequestedToRestart)
            ComposeInvalidator.CancelInvalidate(this);
        _isRequestedToRestart = false;
        _isDisposed = true;
        Group = null;
        VisualElement = null;
        RestartCallback = null;
        Writer = null;
        AncestorLocalGroup = null;
        _capturedStates?.Clear();
        Pool.Return(this);
    }

    private void AssertNotDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException("Trying to access disposed SturdyComposeRestartScope!");
    }

    public override string ToString()
    {
        return $"SturdyComposeRestartScope()[{GetHashCode()}]";
    }
}