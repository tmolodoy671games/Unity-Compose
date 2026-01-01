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

internal class ComposeRestartScope : IScopeUpdateScope, IDisposable
{
    private static readonly NewObjectPool<ComposeRestartScope> _pool = new(() => new());

    private SlotTableWriter _writer = null!;
    public AnchorId _groupAnchor = default!;
    private CompositionLocalMap? _compositionLocalMap = null!;
    private VisualElement? _visualElement = null!;
    private ModifiersStatePair? _modifiers = default!;

    private Action? _restartCallback;
    private int _lastCalledAtFrame = -1;

    public static ComposeRestartScope Get(
        AnchorId groupAnchor,
        SlotTableWriter writer,
        CompositionLocalMap? compositionLocalMap,
        VisualElement? element,
        ModifiersStatePair? modifiers
    )
    {
        var instance = _pool.Get();
        instance._groupAnchor = groupAnchor;
        instance._writer = writer;
        instance._compositionLocalMap = compositionLocalMap;
        instance._visualElement = element;
        instance._modifiers = modifiers;
        return instance;
    }

    private ComposeRestartScope()
    {
    }

    public void SyncFrame()
    {
        _lastCalledAtFrame = Time.frameCount;
    }

    public void UpdateScope(Action restartCallback)
    {
        _restartCallback = restartCallback;
    }

    public void Restart()
    {
        // if (Time.frameCount == _lastCalledAtFrame)
        //     return;
        _lastCalledAtFrame = Time.frameCount;
        _writer.ResetTo(_groupAnchor, _compositionLocalMap, _visualElement, _modifiers);
        _restartCallback?.Invoke();
        _writer.ReleaseCurrentComposer();
    }

    public override string ToString() =>
        $"RestartScope({_groupAnchor}, {_restartCallback != null}, {_compositionLocalMap})";

    public void Dispose()
    {
        ComposeInvalidator.CancelInvalidate(this);
        _pool.Return(this);
    }
}