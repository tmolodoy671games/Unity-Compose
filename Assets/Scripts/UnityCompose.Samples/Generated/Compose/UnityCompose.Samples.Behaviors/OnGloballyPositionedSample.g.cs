using SharpExtensions;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositionedSample : ComposeUI
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.FillMaxSize().Padding(100), content: CurrentComposer.WithState(string.Empty).Remember<System.Action>(__ => () =>
                {
                    var isSwitched = Remember(static () => MutableStateOf(false));
                    var layout = Remember(static () => MutableStateOf(Optional.Empty<Vector2>()));
                    Box(modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState((isSwitched, layout)).Remember<System.Action>(__ => () =>
                    {
                        var transitionSpec = Tween();
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: CurrentComposer.WithState(layout).Remember<System.Action>(__ => () =>
                        {
                            Box(CurrentComposer.WithState(layout).Remember<System.Action>(__ => () =>
                            {
                                Box(CurrentComposer.WithState(layout).Remember<System.Action>(__ => () =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnGloballyPositioned(CurrentComposer.WithState(layout).Remember<System.Action<UnityCompose.LayoutCoordinates>>(__ => it => layout.Value = it.GlobalCenter)));
                                }));
                            }));
                        }));
                    }));
                    Text(modifier: Modifier.Background(Color.blue).Padding(32).Border(32).OnClick(CurrentComposer.WithState(isSwitched).Remember<System.Action>(__ => () => isSwitched.Value = !isSwitched.Value)), color: Color.white, text: "Switch");
                    if (layout.Value.HasValue)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Size(10).Background(Color.red).Float().Position(left: measurer.GlobalToLocal(layout.Value.Value).x, top: measurer.GlobalToLocal(layout.Value.Value).y));
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