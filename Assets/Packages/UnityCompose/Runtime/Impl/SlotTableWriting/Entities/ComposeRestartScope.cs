using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal class ComposeRestartScope : IScopeUpdateScope
{
    public readonly AnchorId _groupAnchor;
    public readonly Dictionary<ICompositionLocal, IMutableState<object?>>? CompositionLocalMap;
    private Action? _restartCallback;
    private readonly SlotTableWriter _writer;

    internal ComposeRestartScope(
        AnchorId groupAnchor,
        SlotTableWriter writer,
        Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap
    )
    {
        _groupAnchor = groupAnchor;
        _writer = writer;
        CompositionLocalMap = compositionLocalMap;
    }

    public void UpdateScope(Action restartCallback)
    {
        _restartCallback = restartCallback;
    }

    public void Restart()
    {
        _writer.ResetTo(_groupAnchor, CompositionLocalMap);
        _restartCallback?.Invoke();
    }

    public override string ToString() =>
        $"RestartScope({_groupAnchor}, {_restartCallback != null}, {CompositionLocalMap?.ToImmutableStableDictionary()})";
}