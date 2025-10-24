using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    Box(modifier: Modifier.Padding(horizontal: isHovered.Value ? 80 : 40, vertical: 16, transition: Transition()).Background(Color.blue).Border(radius: 16).OnMouseEnter(Remember<global::System.Action>(isHovered, () => isHovered.Value = true)).OnMouseLeave(Remember<global::System.Action>(isHovered, () => isHovered.Value = false)), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        Text(text: "Click me", fontSize: 24);
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