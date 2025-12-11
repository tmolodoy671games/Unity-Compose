// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class SingletonState
{
    public static readonly SingletonState Instance = new();

    private SingletonState()
    {
    }
}