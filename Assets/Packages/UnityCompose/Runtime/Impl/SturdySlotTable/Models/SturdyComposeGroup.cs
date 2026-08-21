using System;
using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdyComposeGroup : IDisposable
{
    private static class Pool
    {
        public static SturdyComposeGroup GetOrCreate() => new(); // Pool
    }

    public static SturdyComposeGroup Get() => Pool.GetOrCreate();
    
    public int Key { get; set; }
    public SturdyComposeGroupType Type { get; set; }
    public SturdyComposeGroup? Parent { get; set; }
    public List<SturdyComposeGroup> Children { get; } = new(); // Pool
    public SturdySlots Slots { get; } = SturdySlots.Get();

    private SturdyComposeGroup()
    {
    }

    public void Dispose()
    {
        foreach (var child in Children)
            child.Dispose();
        foreach (var slot in Children)
        {
            if (slot is IDisposable disposable)
                disposable.Dispose();
        }
    }
}

internal enum SturdyComposeGroupType
{
    Replace,
    Restart,
    Reusable,
    Local,
    Movable,
}