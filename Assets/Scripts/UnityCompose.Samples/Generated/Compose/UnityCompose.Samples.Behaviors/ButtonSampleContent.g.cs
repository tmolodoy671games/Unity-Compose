using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    Box(modifier: Modifier.Padding(horizontal: isHovered.Value ? 80 : 40, vertical: 16, transition: Transition()).Background(Color.blue).Border(radius: 16).OnMouseEnter(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(isHovered).Remember<System.Action>(__ => () => isHovered.Value = false)), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                    {
                        CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                        {
                            Text(text: "Click me", fontSize: 24);
                        }));
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