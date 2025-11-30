using System;
using System.Runtime.CompilerServices;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __Key(object key, [Composable] Action content)
    {
        // BRUH
        if (CurrentComposer.BeginComposeGroup(1337, 0, key))
            return;
        try
        {
            content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<object, System.Action>, System.Action>(-227514316, (key, content)) ? CurrentComposer.RememberedValue<ValueTuple<object, System.Action>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<object, System.Action>, System.Action>(() => Key(key, content)));
        }
    }
}