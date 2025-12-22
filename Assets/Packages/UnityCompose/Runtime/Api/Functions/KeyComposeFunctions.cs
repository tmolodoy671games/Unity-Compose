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
        // composer.StartRestartGroup(437, key);
        KeyImpl(content);
    }

    [Composable]
    private static void KeyImpl(ComposableContent content)
    {
        content();
    }
}