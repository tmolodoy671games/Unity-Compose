#nullable enable
using System;
using SharpExtensions;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.BuildUpPerformanceTest
{
    internal partial class ComposeBuildUpPerformanceTest
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-378830448);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-378830448, __isRestarted)?.UpdateScope(() => __Content());
        }
    }
}