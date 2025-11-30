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
            if (CurrentComposer.BeginComposeGroup(889209525, true))
                return;
            try
            {
                Box(CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-291042615, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(889309525, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }
    }
}