using System;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private static void __Key(object key, [Composable] Action content)
    {
        if (CurrentComposer.BeginComposeGroup((key, content), key: key))
            return;
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(Remember<global::System.Action>((key, content), () => Key(key, content)));
        }
    }
}