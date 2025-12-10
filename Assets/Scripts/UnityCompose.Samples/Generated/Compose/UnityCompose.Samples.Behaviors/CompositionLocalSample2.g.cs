using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample2
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(889209525);
            if (__composer.ShouldExecute(true))
            {
                Box(!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(889209525)?.UpdateScope(() => __Content());
        }
    }
}