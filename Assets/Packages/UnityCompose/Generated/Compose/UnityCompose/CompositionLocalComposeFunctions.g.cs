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
        __composer.StartRestartGroup(-1174149353);
        if (__composer.ShouldExecute((__provides, __content)))
        {
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1174149353)?.UpdateScope(() => __CompositionLocalProvider(__provides, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, ComposableContent content)
    {
        var(__provides1, __content) = (provides1, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1808499173);
        if (__composer.ShouldExecute((__provides1, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<UnityCompose.CompositionLocalProvides>(903371336, provides1) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1808499173)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, ComposableContent content)
    {
        var(__provides1, __provides2, __content) = (provides1, provides2, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(93574391);
        if (__composer.ShouldExecute((__provides1, __provides2, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2)>(-1931431393, (provides1, provides2)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(93574391)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __content) = (provides1, provides2, provides3, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1366206322);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3)>(1436494912, (provides1, provides2, provides3)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1366206322)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __content) = (provides1, provides2, provides3, provides4, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1494379587);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __provides4, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4)>(-1008313329, (provides1, provides2, provides3, provides4)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1494379587)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __content) = (provides1, provides2, provides3, provides4, provides5, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(579922586);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __provides4, __provides5, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5)>(838968099, (provides1, provides2, provides3, provides4, provides5)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(579922586)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(547449752);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6)>(865599160, (provides1, provides2, provides3, provides4, provides5, provides6)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(547449752)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1896130750);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7)>(-1836261197, (provides1, provides2, provides3, provides4, provides5, provides6, provides7)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1896130750)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-446850960);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8)>(683989053, (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-446850960)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider(CompositionLocalProvides provides1, CompositionLocalProvides provides2, CompositionLocalProvides provides3, CompositionLocalProvides provides4, CompositionLocalProvides provides5, CompositionLocalProvides provides6, CompositionLocalProvides provides7, CompositionLocalProvides provides8, CompositionLocalProvides provides9, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-451116254);
        if (__composer.ShouldExecute((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)))
        {
            var provides = !__composer.RememberedKeyChanged<(UnityCompose.CompositionLocalProvides provides1, UnityCompose.CompositionLocalProvides provides2, UnityCompose.CompositionLocalProvides provides3, UnityCompose.CompositionLocalProvides provides4, UnityCompose.CompositionLocalProvides provides5, UnityCompose.CompositionLocalProvides provides6, UnityCompose.CompositionLocalProvides provides7, UnityCompose.CompositionLocalProvides provides8, UnityCompose.CompositionLocalProvides provides9)>(1032894183, (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.CompositionLocalProvides>>(IImmutableStableList.Create(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9));
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-451116254)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content));
    }

    [Composable]
    private static void __CompositionLocalProviderImpl(IImmutableStableList<CompositionLocalProvides> provides, ComposableContent content)
    {
        var(__provides, __content) = (provides, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1042753624);
        if (__composer.ShouldExecute((__provides, __content)))
        {
            CurrentComposer.UpdateCompositionLocal(provides);
            content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1042753624)?.UpdateScope(() => __CompositionLocalProviderImpl(__provides, __content));
    }
}