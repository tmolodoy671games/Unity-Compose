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
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }
    }
}