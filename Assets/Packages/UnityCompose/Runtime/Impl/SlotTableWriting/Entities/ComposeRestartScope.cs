using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

public class ComposeRestartScope : IScopeUpdateScope
{
    public readonly AnchorId _groupAnchor;
    private Action? _restartCallback;
    private readonly ISlotTableWriter _writer;

    internal ComposeRestartScope(AnchorId groupAnchor, ISlotTableWriter writer)
    {
        _groupAnchor = groupAnchor;
        _writer = writer;
    }

    public void UpdateScope(Action restartCallback)
    {
        _restartCallback = restartCallback;
    }

    public void Restart()
    {
        _writer.ResetTo(_groupAnchor);
        _restartCallback?.Invoke();
    }

    public override string ToString() => $"RestartScope({_groupAnchor}, {_restartCallback != null})";
}