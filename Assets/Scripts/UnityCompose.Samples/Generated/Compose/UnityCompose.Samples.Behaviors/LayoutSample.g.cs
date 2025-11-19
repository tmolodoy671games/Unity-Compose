using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class LayoutSample
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    Box(modifier: Modifier.Background(Color.red).Size(400), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                    {
                        Spacer(modifier: Modifier.Size(100).Float().Background(Color.yellow).Position(top: 5));
                        Spacer(modifier: Modifier.Size(100).Float().Background(Color.yellow).Position(bottom: 5));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}