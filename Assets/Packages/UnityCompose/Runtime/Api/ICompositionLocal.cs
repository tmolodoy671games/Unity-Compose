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
    private readonly string? _name;

    public CompositionLocalImpl(string? name, Func<T> defaultValueFactory)
    {
        _defaultValueFactory = defaultValueFactory;
        _name = name;
    }

    public T Current
    {
        [Composable] get => CurrentComposer.GetCompositionLocal(this, _defaultValueFactory);
    }

    public override string ToString()
    {
        return _name ?? $"ICompositionLocal<{typeof(T).Name}>";
    }
}

public readonly record struct CompositionLocalProvides(
    ICompositionLocal CompositionLocal,
    object? Value
)
{
    public override string ToString()
    {
        return $"{CompositionLocal} Provides {Value}";
    }
}