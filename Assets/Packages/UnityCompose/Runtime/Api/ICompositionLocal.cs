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
        [Composable, Compiled]
        get => ComposeFunctions.CurrentComposer.GetCompositionLocal(this, _defaultValueFactory);
    }
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
}