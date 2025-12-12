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
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, ComposableContent content)
    {
        var(__provides1, __provides2, __content) = (provides1, provides2, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1600544323);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1600544323)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __content) = (provides1, provides2, provides3, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1374181071);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1374181071)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __content) = (provides1, provides2, provides3, provides4, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1525559122);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1525559122)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __content) = (provides1, provides2, provides3, provides4, provides5, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1815871177);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1815871177)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(53246200);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(53246200)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-293011939);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6, provides7)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-293011939)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(863628257);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(863628257)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, CompositionLocalProvides provides9, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-710632620);
        if (__composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)))
        {
            var provides = !__composer.ChangedAsStruct((provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-710632620)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content));
    }

    [Composable]
    private static void __CompositionLocalProviderImpl(IImmutableStableList<CompositionLocalProvides> provides, ComposableContent content)
    {
        var(__provides, __content) = (provides, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1510366283);
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

        __composer.EndRestartGroup(1510366283)?.UpdateScope(() => __CompositionLocalProviderImpl(__provides, __content));
    }
}