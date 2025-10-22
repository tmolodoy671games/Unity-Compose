using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IDisposableEffectScope
{
    IDisposable OnDispose(Action onDispose);
}