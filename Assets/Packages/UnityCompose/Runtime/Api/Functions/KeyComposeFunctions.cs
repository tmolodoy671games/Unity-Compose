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
        composer.StartRestartGroup(4357447, key);
        var isRestarted = composer.IsRestarted();
        if (isRestarted || composer.ShouldExecuteAsStruct((key, content)))
        {
            content();
        }
        else
        {
            composer.SkipToGroupEnd();
        }

        composer.EndRestartGroup(4357447, isRestarted)?.UpdateScope(() => Key(key, content));
    }
}