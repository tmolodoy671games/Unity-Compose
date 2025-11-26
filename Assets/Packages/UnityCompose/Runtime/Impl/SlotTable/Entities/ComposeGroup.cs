using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Entities;

internal abstract class ComposeGroup : IDisposable
{
    public int Key;
    public readonly ReusableComposeGroup? Parent;
    public int IndexInParent;

    internal ComposeGroup(int key, ReusableComposeGroup? parent)
    {
        Key = key;
        Parent = parent;
    }

    public virtual string ToString(ComposeGroup? currentParent, int currentIndex) =>
        $"{Key} {IndexInParent} {GetType().Name} Not implemented";

    public abstract void Dispose();
}