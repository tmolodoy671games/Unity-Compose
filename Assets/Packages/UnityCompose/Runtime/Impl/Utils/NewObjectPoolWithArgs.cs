using System;
using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

internal class NewObjectPool<TArg, T>
{
    private readonly Stack<T> _pool = new();
    private readonly Func<TArg, T> _factory;
    private readonly Action<T>? _onInit;
    private readonly Action<T>? _onRelease;

    public NewObjectPool(Func<TArg, T> factory, Action<T>? onInit = null, Action<T>? onRelease = null)
    {
        _factory = factory;
        _onInit = onInit;
        _onRelease = onRelease;
    }

    public T Get(TArg arg)
    {
        if (_pool.TryPop(out var pooledInstance))
        {
            _onInit?.Invoke(pooledInstance);
            return pooledInstance;
        }

        var newInstance = _factory(arg);
        _onInit?.Invoke(newInstance);
        return newInstance;
    }

    public void Release(T instance)
    {
        _onRelease?.Invoke(instance);
        _pool.Push(instance);
    }
}