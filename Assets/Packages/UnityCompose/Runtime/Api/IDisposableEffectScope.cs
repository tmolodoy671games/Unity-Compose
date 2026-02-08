using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IDisposableEffectScope
{
    IDisposableEffectResult OnDispose(Action onDispose);
}

public interface IComposeDisposable
{
    void Dispose();
}

public interface IDisposableEffectResult : IComposeDisposable
{
}