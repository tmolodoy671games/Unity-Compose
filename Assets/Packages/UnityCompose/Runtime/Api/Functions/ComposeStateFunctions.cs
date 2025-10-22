using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IMutableState<T> MutableStateOf<T>(T value)
    {
        return new MutableStateImpl<T>(value);
    }
    
    internal static IMutableState<T> MutableStateOf<T>(T value, bool isCompositionLocal)
    {
        return new MutableStateImpl<T>(value, isCompositionLocal);
    }

    public static IMutableStateList<T> MutableStateListOf<T>()
    {
        return new MutableStateListImpl<T>();
    }

    public static IMutableStateList<T> MutableStateListOf<T>(params T[] values)
    {
        return new MutableStateListImpl<T>(values);
    }

    public static IMutableStateList<T> MutableStateListOf<T>(IEnumerable<T> values)
    {
        return new MutableStateListImpl<T>(values);
    }

    public static IMutableStateSet<T> MutableStateSetOf<T>()
    {
        return new MutableStateSetImpl<T>();
    }

    public static IMutableStateSet<T> MutableStateSetOf<T>(params T[] values)
    {
        return new MutableStateSetImpl<T>(values);
    }

    public static IMutableStateDictionary<TKey, TValue> MutableStateDictionaryOf<TKey, TValue>()
    {
        return new MutableStateDictionaryImpl<TKey, TValue>();
    }

    public static IMutableStateDictionary<TKey, TValue> MutableStateDictionaryOf<TKey, TValue>(
        params (TKey, TValue)[] values)
    {
        return new MutableStateDictionaryImpl<TKey, TValue>(values);
    }
}