using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ManualLayoutDemo
    {
        [Composable]
        private static void __MockLayout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1422153076);
            if (__composer.ShouldExecute(true))
            {
                __composer.StartReplaceGroup(847579505);
                for (var i = 0; i < 1_000_000; i++)
                {
                    CurrentComposer.StartRestartGroup(i);
                    CurrentComposer.EndRestartGroup(i);
                // Spacer(Modifier);
                }

                __composer.EndReplaceGroup(847579505);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1422153076)?.UpdateScope(() => __MockLayout());
        }
    }
}