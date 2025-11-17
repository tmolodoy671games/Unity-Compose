using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent : ComposeUI
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: static () =>
                {
                    var isHovered = Remember(static () => MutableStateOf(false));
                    Box(modifier: Modifier.Padding(horizontal: isHovered.Value ? 80 : 40, vertical: 16, transition: Transition()).Background(Color.blue).Border(radius: 16).OnMouseEnter(CurrentComposer.WithState(isHovered).Remember<Action>(__ => () => isHovered.Value = true)).OnMouseLeave(CurrentComposer.WithState(isHovered).Remember<Action>(__ => () => isHovered.Value = false)), content: static () =>
                    {
                        CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: static () =>
                        {
                            Text(text: "Click me", fontSize: 24);
                        });
                    });
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }
}