using System;
using System.Linq;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Utils;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal abstract class ReplaceableComposeGroup : ComposeGroup
{
    protected ReplaceableComposeGroup(int key, ReusableComposeGroup? parent) : base(key, parent)
    {
    }
}

interface IReplaceableKeyComposeGroup<TKey>
{
    Optional<TKey> Key { get; }
}

internal class ReplaceableComposeGroup<TKey, TValue> : ReplaceableComposeGroup
{
    public Optional<TKey> CacheKey;
    public TValue Value = default!;

    public ReplaceableComposeGroup(int key, ReusableComposeGroup? parent) : base(key, parent)
    {
    }

    public override void Dispose()
    {
        if (Value is IDisposable disposable)
            disposable.Dispose();
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        var ancestorsCount = this.Ancestors().Count();
        builder.Append("-".Multiply(ancestorsCount));
        builder.Append($"[{IndexInParent}] ");
        builder.Append($"(Key: {Key}, IndexInParent: {IndexInParent}, CacheKey: {CacheKey}, Value: {Value})");
        return builder.ToString();
    }
}