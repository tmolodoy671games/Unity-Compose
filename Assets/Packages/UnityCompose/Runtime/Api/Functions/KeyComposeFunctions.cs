// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static void Key<T>(
        T key,
        ComposableContent content
    )
    {
        var composer = CurrentComposer;
        var intKey = key?.GetHashCode() ?? 0;
        composer.StartMovableGroup(intKey, key);
        KeyImpl(content);
        composer.EndMovableGroup(intKey);
    }

    [Composable]
    private static void KeyImpl(ComposableContent content)
    {
        content();
    }
}