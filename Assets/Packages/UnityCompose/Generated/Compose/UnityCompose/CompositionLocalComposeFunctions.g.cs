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
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(185578461);
        if (__composer.ShouldExecuteAsStruct((__provides, __content)))
        {
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(185578461)?.UpdateScope(() => __CompositionLocalProvider(__provides, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, ComposableContent content)
    {
        var(__provides1, __content) = (provides1, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1189998356);
        if (__composer.ShouldExecuteAsStruct((__provides1, __content)))
        {
            var provides = !__composer.ChangedAsStruct(provides1) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1189998356)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __content));
    }

    [Composable]
    private static void __LoggableCompositionLocalProvider(CompositionLocalProvides provides1, ComposableContent content)
    {
        var(__provides1, __content) = (provides1, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1600544323);
        if (__composer.ShouldExecuteAsStruct((__provides1, __content)))
        {
            var provides = !__composer.ChangedAsStruct(provides1) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1));
            LoggableCompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1600544323)?.UpdateScope(() => __LoggableCompositionLocalProvider(__provides1, __content));
    }

    [Composable]
    private static void __LoggableCompositionLocalProviderImpl(IImmutableStableList<CompositionLocalProvides> provides, ComposableContent content)
    {
        var(__provides, __content) = (provides, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1374181071);
        if (__composer.ShouldExecuteAsStruct((__provides, __content)))
        {
            CurrentComposer.StartLocalGroup(123);
            CurrentComposer.UpdateCompositionLocal(provides);
            content();
            CurrentComposer.EndLocalGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1374181071)?.UpdateScope(() => __LoggableCompositionLocalProviderImpl(__provides, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, ComposableContent content)
    {
        var(__provides1, __provides2, __content) = (provides1, provides2, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-485029187);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-485029187)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __content) = (provides1, provides2, provides3, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-587139638);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-587139638)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __content) = (provides1, provides2, provides3, provides4, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(835843544);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(835843544)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __content) = (provides1, provides2, provides3, provides4, provides5, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(995095908);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(995095908)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1461942799);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1461942799)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1340101204);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6, provides7)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1340101204)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1416436347);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1416436347)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, CompositionLocalProvides provides9, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(2071621430);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(2071621430)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content));
    }

    [Composable]
    private static void __CompositionLocalProviderImpl(IImmutableStableList<CompositionLocalProvides> provides, ComposableContent content)
    {
        var(__provides, __content) = (provides, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-691166404);
        if (__composer.ShouldExecuteAsStruct((__provides, __content)))
        {
            CurrentComposer.StartLocalGroup(123);
            CurrentComposer.UpdateCompositionLocal(provides);
            content();
            CurrentComposer.EndLocalGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-691166404)?.UpdateScope(() => __CompositionLocalProviderImpl(__provides, __content));
    }
}