using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class ComposeRestartScope : IScopeUpdateScope, IDisposable
{
    private readonly SlotTableWriter _writer;
    public readonly AnchorId _groupAnchor;
    private readonly Dictionary<ICompositionLocal, IMutableState<object?>>? _compositionLocalMap;
    private readonly VisualElement? _visualElement;
    private readonly ModifiersPair _modifiers;

    private Action? _restartCallback;
    private int _lastCalledAtFrame = -1;

    internal ComposeRestartScope(
        AnchorId groupAnchor,
        SlotTableWriter writer,
        Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap,
        VisualElement? element,
        ModifiersPair modifiers
    )
    {
        _groupAnchor = groupAnchor;
        _writer = writer;
        _compositionLocalMap = compositionLocalMap;
        _visualElement = element;
        _modifiers = modifiers;
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
        if (Time.frameCount == _lastCalledAtFrame)
            return;
        _lastCalledAtFrame = Time.frameCount;
        _writer.ResetTo(_groupAnchor, _compositionLocalMap, _visualElement, _modifiers);
        _restartCallback?.Invoke();
        _writer.ReleaseCurrentComposer();
    }

    public override string ToString() =>
        $"RestartScope({_groupAnchor}, {_restartCallback != null}, {_compositionLocalMap?.ToImmutableStableDictionary()})";

    public void Dispose()
    {
        ComposeInvalidator.CancelInvalidate(this);
    }
}