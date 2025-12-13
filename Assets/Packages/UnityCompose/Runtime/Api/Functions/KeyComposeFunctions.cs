// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static void Key<T>(
        T key,
        ComposableContent content
    )
    {
        // BRUH
        content();
    }
}