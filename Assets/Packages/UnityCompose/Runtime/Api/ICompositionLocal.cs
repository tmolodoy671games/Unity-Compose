using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface ICompositionLocal
{
}

public interface ICompositionLocal<out T> : ICompositionLocal
{
    T Current { get; }
}

internal class CompositionLocalImpl<T> : ICompositionLocal<T>
{
    private readonly Func<T> _defaultValueFactory;

    public CompositionLocalImpl(Func<T> defaultValueFactory)
    {
        _defaultValueFactory = defaultValueFactory;
    }

    public T Current
    {
        [Composable, Compiled] get => ComposeFunctions.CurrentComposer.GetCompositionLocal(this, _defaultValueFactory);
    }
}

internal class MappedCompositionLocalImpl<T1, T2> : ICompositionLocal<T2>
{
    private readonly ICompositionLocal<T1> _original;
    private readonly Func<T1, T2> _selector;

    public MappedCompositionLocalImpl(ICompositionLocal<T1> original, Func<T1, T2> selector)
    {
        _original = original;
        _selector = selector;
    }

    public T2 Current => _selector(_original.Current);
}

public readonly struct CompositionLocalProvides
{
    public readonly ICompositionLocal CompositionLocal;
    public readonly object? Value;

    public CompositionLocalProvides(ICompositionLocal compositionLocal, object? value)
    {
        CompositionLocal = compositionLocal;
        Value = value;
    }
}

public static class CompositionLocalExtensions
{
    public static CompositionLocalProvides Provides<T>(this ICompositionLocal<T> compositionLocal, T value)
    {
        return new CompositionLocalProvides(compositionLocal, value);
    }

    public static ICompositionLocal<T2> Select<T1, T2>(
        this ICompositionLocal<T1> compositionLocal,
        Func<T1, T2> selector
    )
    {
        return new MappedCompositionLocalImpl<T1, T2>(compositionLocal, selector);
    }
}