using System;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public interface IFlow<out T>
{
    IDisposable Collect(Action<T> collect);
}

internal class MutableFlowImpl<T> : IFlow<T>
{
    private readonly IMutableStableList<Action<T>> _subscribers = MutableStableListOf<Action<T>>();
    
    public void Emit(T value)
    {
        foreach (var subscriber in _subscribers)
            subscriber(value);
    }
    
    public IDisposable Collect(Action<T> collect)
    {
        _subscribers.Add(collect);
        return new CustomDisposable(() =>
        {
            _subscribers.Remove(collect);
        });
    }
}