using System.Collections;
using System.Collections.Generic;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IStateDictionary<TKey, TValue> : IStableDictionary<TKey, TValue>
{
}

public interface IMutableStateDictionary<TKey, TValue> : IStateDictionary<TKey, TValue>,
    IMutableStableDictionary<TKey, TValue>
{
}

internal class MutableStateDictionaryImpl<TKey, TValue> : BaseMutableStateImpl,
    IMutableStateDictionary<TKey, TValue>
{
    private readonly IMutableStableDictionary<TKey, TValue> _dictionary;

    public MutableStateDictionaryImpl()
    {
        _dictionary = IMutableStableDictionary.Create<TKey, TValue>();
    }

    public MutableStateDictionaryImpl(params (TKey, TValue)[] entries)
    {
        _dictionary = IMutableStableDictionary.Create(entries);
    }

    public MutableStateDictionaryImpl(IEnumerable<(TKey, TValue)> entries)
    {
        _dictionary = entries.ToMutableStableDictionary();
    }

    IStableDictionary<TKey, TValue>.Enumerator IStableDictionary<TKey, TValue>.GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    public int Count
    {
        get
        {
            Capture();
            return _dictionary.Count;
        }
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            Capture();
            return _dictionary.Keys;
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            Capture();
            return _dictionary.Values;
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            Capture();
            return _dictionary[key];
        }
        set
        {
            if (TryGet(key, out var cachedValue) && EqualityComparer<TValue>.Default.Equals(cachedValue, value))
                return;
            _dictionary[key] = value;
            Notify();
        }
    }

    public void Clear()
    {
        var result = _dictionary.IsNotEmpty;
        _dictionary.Clear();
        if (result)
            Notify();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        Capture();
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool ContainsKey(TKey key)
    {
        Capture();
        return _dictionary.ContainsKey(key);
    }

    public bool TryGet(TKey key, out TValue value)
    {
        Capture();
        return _dictionary.TryGet(key, out value);
    }

    public bool Remove(TKey key)
    {
        var result = _dictionary.Remove(key);
        if (result)
            Notify();
        return result;
    }

    public int AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        var result = _dictionary.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(IEnumerable<(TKey Key, TValue Value)> items)
    {
        var result = _dictionary.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(params KeyValuePair<TKey, TValue>[] items)
    {
        var result = _dictionary.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int AddRange(params (TKey Key, TValue Value)[] items)
    {
        var result = _dictionary.AddRange(items);
        if (result != 0)
            Notify();
        return result;
    }

    public int RemoveRange(IEnumerable<TKey> keys)
    {
        var result = _dictionary.RemoveRange(keys);
        if (result != 0)
            Notify();
        return result;
    }

    public int RemoveRange(params TKey[] keys)
    {
        var result = _dictionary.RemoveRange(keys);
        if (result != 0)
            Notify();
        return result;
    }

    public override string ToString()
    {
        Capture();
        return _dictionary.ToString();
    }
}