using System;

// ReSharper disable CheckNamespace

namespace UnityCompose;

public interface IScopeUpdateScope
{
    void UpdateScope(Action restartCallback);
}