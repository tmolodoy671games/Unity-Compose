using System;
using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __CompositionLocalProvider<T1>(CompositionLocalProvides<T1> provides1, ComposableContent content)
    {
        var(__provides1, __content) = (provides1, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-703960699);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, Unit, Unit, Unit, Unit, Unit, Unit, Unit, Unit, Unit>());
            provides.Update(provides1);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-703960699, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, ComposableContent content)
    {
        var(__provides1, __provides2, __content) = (provides1, provides2, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-2015728891);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, Unit, Unit, Unit, Unit, Unit, Unit, Unit, Unit>());
            provides.Update(provides1, provides2);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-2015728891, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __content) = (provides1, provides2, provides3, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-896819872);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, Unit, Unit, Unit, Unit, Unit, Unit, Unit>());
            provides.Update(provides1, provides2, provides3);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-896819872, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __content) = (provides1, provides2, provides3, provides4, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1861977477);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, T4, Unit, Unit, Unit, Unit, Unit, Unit>());
            provides.Update(provides1, provides2, provides3, provides4);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1861977477, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4, T5>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __content) = (provides1, provides2, provides3, provides4, provides5, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1617866357);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, T4, T5, Unit, Unit, Unit, Unit, Unit>());
            provides.Update(provides1, provides2, provides3, provides4, provides5);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1617866357, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(369200937);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, Unit, Unit, Unit, Unit>());
            provides.Update(provides1, provides2, provides3, provides4, provides5, provides6);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(369200937, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1780236285);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, UnityCompose.Unit, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, Unit, Unit, Unit>());
            provides.Update(provides1, provides2, provides3, provides4, provides5, provides6, provides7);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1780236285, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, CompositionLocalProvides<T8> provides8, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1093146676);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, UnityCompose.Unit, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, UnityCompose.Unit, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, Unit, Unit>());
            provides.Update(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1093146676, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8, T9>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, CompositionLocalProvides<T8> provides8, CompositionLocalProvides<T9> provides9, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1383627550);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, UnityCompose.Unit>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, UnityCompose.Unit>>(new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, Unit>());
            provides.Update(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1383627550, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __content));
    }

    [Composable]
    private static void __CompositionLocalProvider<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(CompositionLocalProvides<T1> provides1, CompositionLocalProvides<T2> provides2, CompositionLocalProvides<T3> provides3, CompositionLocalProvides<T4> provides4, CompositionLocalProvides<T5> provides5, CompositionLocalProvides<T6> provides6, CompositionLocalProvides<T7> provides7, CompositionLocalProvides<T8> provides8, CompositionLocalProvides<T9> provides9, CompositionLocalProvides<T10> provides10, ComposableContent content)
    {
        var(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __provides10, __content) = (provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, provides10, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1822671578);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __provides10, __content)))
        {
            var provides = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>() : __composer.UpdateRememberedValue<UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities.CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(new CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>());
            provides.Update(provides1, provides2, provides3, provides4, provides5, provides6, provides7, provides8, provides9, provides10);
            CompositionLocalProviderImpl(provides, content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1822671578, __isRestarted)?.UpdateScope(() => __CompositionLocalProvider(__provides1, __provides2, __provides3, __provides4, __provides5, __provides6, __provides7, __provides8, __provides9, __provides10, __content));
    }
}