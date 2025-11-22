using System;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class ComposeGroupRestartScope
{
    private readonly SlotWriter _writer;
    public int GroupIndex;
    public Action? Restart;

    public ComposeGroupRestartScope(SlotWriter writer)
    {
        _writer = writer;
    }

    public void PerformRestart()
    {
        _writer.ResetTo(GroupIndex);
        Restart?.Invoke();
    }
}