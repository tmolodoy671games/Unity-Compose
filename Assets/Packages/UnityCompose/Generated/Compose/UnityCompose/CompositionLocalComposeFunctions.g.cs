using System;
using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __CompositionLocalProvider(IImmutableStableList<CompositionLocalProvides> provides, ComposableContent content)
    {
        var(__provides, __content) = (provides, content);
        if (CurrentComposer.BeginComposeGroup(1015604604, (__provides, __content)))
            return;
        try
        {
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<IImmutableStableList<CompositionLocalProvides>, ComposableContent>, Action>(1015704604, (__provides, __content)) ? CurrentComposer.RememberedValue<ValueTuple<IImmutableStableList<CompositionLocalProvides>, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<IImmutableStableList<CompositionLocalProvides>, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides, __content)));
        }
    }

    [Composable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, ComposableContent content)
    {
        var(__provides1, __content) = (provides1, content);
        if (CurrentComposer.BeginComposeGroup(-867718595, (__provides1, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<UnityCompose.CompositionLocalProvides, StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(-1753241669, provides1) ? CurrentComposer.RememberedValue<UnityCompose.CompositionLocalProvides, StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<UnityCompose.CompositionLocalProvides, StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, ComposableContent>, Action>(-867618595, (__provides1, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides1, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, ComposableContent content)
    {
        var(__provides1, __provides2, __content) = (provides1, provides2, content);
        if (CurrentComposer.BeginComposeGroup(713770753, (__provides1, __provides2, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(-458647162, (provides1, provides2)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(713870753, (__provides1, __provides2, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __content) = (provides1, provides2, provides3, content);
        if (CurrentComposer.BeginComposeGroup(-1729294312, (__provides1, __provides2, __provides3, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(-2036995216, (provides1, provides2, provides3)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(-1729194312, (__provides1, __provides2, __provides3, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __content) = (provides1, provides2, provides3, provides4, content);
        if (CurrentComposer.BeginComposeGroup(286843146, (__provides1, __provides2, __provides3, __provides4, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(2132955880, (provides1, provides2, provides3, provides4)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3, provides4));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(286943146, (__provides1, __provides2, __provides3, __provides4, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __content) = (provides1, provides2, provides3, provides4, provides5, content);
        if (CurrentComposer.BeginComposeGroup(734437258, (__provides1, __provides2, __provides3, __provides4, __provides5, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(-1381683461, (provides1, provides2, provides3, provides4, provides5)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(734537258, (__provides1, __provides2, __provides3, __provides4, __provides5, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, content);
        if (CurrentComposer.BeginComposeGroup(835843544, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(978421828, (provides1, provides2, provides3, provides4, provides5, provides6)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(835943544, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ComposableContent>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, content);
        if (CurrentComposer.BeginComposeGroup(-606371864, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(-2047268371, (provides1, provides2, provides3, provides4, provides5, provides6, provides7)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<ComposableContent>>, Action>(-606271864, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<ComposableContent>>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<ComposableContent>>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, content);
        if (CurrentComposer.BeginComposeGroup(1567144239, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(1340101204, (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<CompositionLocalProvides, ComposableContent>>, Action>(1567244239, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<CompositionLocalProvides, ComposableContent>>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<CompositionLocalProvides, ComposableContent>>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)));
        }
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, CompositionLocalProvides provides9, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, content);
        if (CurrentComposer.BeginComposeGroup(-351322347, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)))
            return;
        try
        {
            var provides = CurrentComposer.HasRememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8, UnityCompose.CompositionLocalProvides provides9), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(-956236604, (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9)) ? CurrentComposer.RememberedValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8, UnityCompose.CompositionLocalProvides provides9), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : CurrentComposer.WriteValue<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8, UnityCompose.CompositionLocalProvides provides9), StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(() => IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9));
            CompositionLocalProviderImpl(provides, content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<CompositionLocalProvides, CompositionLocalProvides, ComposableContent>>, Action>(-351222347, (__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)) ? CurrentComposer.RememberedValue<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<CompositionLocalProvides, CompositionLocalProvides, ComposableContent>>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, CompositionLocalProvides, ValueTuple<CompositionLocalProvides, CompositionLocalProvides, ComposableContent>>, Action>(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)));
        }
    }
}