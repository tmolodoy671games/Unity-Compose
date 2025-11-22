
using System;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal interface IRememberedValue {}

internal class RememberedValue<TKey, TValue> : IDisposable, IRememberedValue
{
    public Optional<TKey> Key { get; set; }
    public TValue Value { get; set; } = default!;

    public override string ToString()
    {
        return $"({Key}: {Value})";
    }

    public void Dispose()
    {
        if (Value != null && Value is IDisposable disposable)
            disposable.Dispose();
    }
}