using System;
using SharpExtensions;
using Sirenix.OdinInspector;
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
            __composer.StartRestartGroup(-409325130);
            if (__composer.ShouldExecute(true))
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-409325130)?.UpdateScope(() => __Content());
        }
    }
}