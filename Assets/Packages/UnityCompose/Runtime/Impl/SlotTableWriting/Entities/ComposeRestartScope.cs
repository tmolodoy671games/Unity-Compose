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
    public AnchorId _groupAnchor;
    private Dictionary<ICompositionLocal, IMutableState<object?>>? _compositionLocalMap;
    private ModifiersStatePair? _modifiers;
    private ElementAnchorId _elementAnchorId;

    private Action? _restartCallback;

    public static ComposeRestartScope Get(
        AnchorId groupAnchor,
        SlotTableWriter writer,
        Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap,
        ModifiersStatePair? modifiers,
        ElementAnchorId elementAnchorId
    )
    {
        var instance = _pool.Get();
        instance._groupAnchor = groupAnchor;
        instance._writer = writer;
        instance._compositionLocalMap = compositionLocalMap;
        instance._elementAnchorId = elementAnchorId;
        instance._modifiers = modifiers;
        return instance;
    }

    private ComposeRestartScope()
    {
    }

    public void UpdateScope(Action restartCallback)
    {
        _restartCallback = restartCallback;
    }

    public void Restart()
    {
        _writer.ResetTo(_groupAnchor, _compositionLocalMap, _modifiers, _elementAnchorId);
        _restartCallback?.Invoke();
        _writer.ReleaseCurrentComposer();
    }

    public override string ToString() =>
        $"RestartScope({_groupAnchor}, {_restartCallback != null}, {_compositionLocalMap?.ToImmutableStableDictionary()})";

    public void Dispose()
    {
        ComposeInvalidator.CancelInvalidate(this);
        _pool.Release(this);
    }
}