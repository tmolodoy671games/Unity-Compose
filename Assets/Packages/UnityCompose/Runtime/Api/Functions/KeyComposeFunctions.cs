using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable, Compiled]
    public static void Key(
        object key,
        [Composable] Action content
    )
    {
        if (CurrentComposer.BeginComposeGroup((key, content), key: key)) return;
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => Key(key, content));
        }
    }
}