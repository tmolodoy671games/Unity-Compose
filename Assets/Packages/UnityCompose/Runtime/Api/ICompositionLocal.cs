using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface ICompositionLocal
{
}

public interface ICompositionLocal<T> : ICompositionLocal
{
    T Current { get; }
    
    public CompositionLocalProvides Provides(T value)
    {
        return new CompositionLocalProvides(this, value);
    }
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
        [Composable] get => CurrentComposer.GetCompositionLocal(this, _defaultValueFactory);
    }

    public override string ToString()
    {
        return typeof(T).Name;
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

    public override string ToString()
    {
        return $"{CompositionLocal} Provides {Value}";
    }
}