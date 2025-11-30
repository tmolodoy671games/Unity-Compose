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
            if (CurrentComposer.BeginComposeGroup(-409325130, true))
                return;
            try
            {
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-409225130, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }
    }
}