#nullable enable
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
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(2069875098);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(2069875098, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }
    }
}