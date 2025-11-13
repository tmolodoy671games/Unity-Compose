// ReSharper disable ArrangeNamespaceBody
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositionedSample
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }

        [Composable]
        [Compiled]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.FillMaxSize().Padding(100), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var isSwitched = Remember(static () => MutableStateOf(false));
                    var layout = Remember(static () => MutableStateOf(Optional.Empty<Vector2>()));
                    Box(modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>((isSwitched, layout), () =>
                    {
                        var transitionSpec = Tween(duration: 1f);
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Margin(left: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: RememberComposable<global::System.Action>(layout, () =>
                        {
                            var measurer = LocalLayoutMeasurer.Current;
                            Box(RememberComposable<global::System.Action>(layout, () =>
                            {
                                Box(RememberComposable<global::System.Action>(layout, () =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnLocallyPositioned(Remember<global::System.Action<global::UnityCompose.LayoutCoordinates>>(layout, it => layout.Value = it.GlobalPosition)));
                                }));
                            }));
                        }));
                    }));
                    Text(modifier: Modifier.Background(Color.blue).Padding(32).Border(32).OnClick(Remember<global::System.Action>(isSwitched, () =>
                    {
                        Debug.Log("OnClick()");
                        isSwitched.Value = !isSwitched.Value;
                    })), color: Color.white, text: "Switch");
                    if (layout.Value.HasValue)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Size(8).Background(Color.red).Float().Position(left: measurer.GlobalToLocal(layout.Value.Value).x, top: measurer.GlobalToLocal(layout.Value.Value).y));
                    }
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}