#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class MinimalSample
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1724393487);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1724393487, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private void __Preview()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(306248645);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(306248645, __isRestarted)?.UpdateScope(() => __Preview());
        }

        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1598254630);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1598254630, __isRestarted)?.UpdateScope(() => __Layout());
        }
    }
}