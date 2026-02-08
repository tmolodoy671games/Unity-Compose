using System;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal static class MutableSlotEntry
{
    public static MutableSlotEntry<T> Get<T>(T initialValue)
    {
        return MutableSlotEntry<T>.Get(initialValue);
    }
}

internal class MutableSlotEntry<T> : IComposeDisposable
{
    private static readonly ObjectPool<MutableSlotEntry<T>> _pool = new(
        factory: static () => new MutableSlotEntry<T>()
    );

    public static MutableSlotEntry<T> Get(T initialValue)
    {
        var result = _pool.Get();
        result._isDisposed = false;
        result.Value = initialValue;
        return result;
    }
    
    public T Value = default!;
    private bool _isDisposed;

    private MutableSlotEntry()
    {
    }

    public override string ToString()
    {
        return $"{Value}";
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        _pool.Return(this);
    }
}