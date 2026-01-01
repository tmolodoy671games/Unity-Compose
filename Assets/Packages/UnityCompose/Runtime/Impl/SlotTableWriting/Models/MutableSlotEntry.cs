using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Models;

internal static class MutableSlotEntry
{
    public static MutableSlotEntry<T> Get<T>(T initialValue)
    {
        return MutableSlotEntry<T>.Get(initialValue);
    }
}

internal class MutableSlotEntry<T> : IDisposable
{
    private static readonly NewObjectPool<MutableSlotEntry<T>> _pool = new(
        factory: static () => new MutableSlotEntry<T>()
    );

    public static MutableSlotEntry<T> Get(T initialValue)
    {
        var result = _pool.Get();
        result.Value = initialValue;
        return result;
    }
    
    public T Value = default!;

    private MutableSlotEntry()
    {
    }

    public override string ToString()
    {
        return $"MutableSlotEntry({Value})";
    }

    public void Dispose()
    {
        _pool.Return(this);
    }
}