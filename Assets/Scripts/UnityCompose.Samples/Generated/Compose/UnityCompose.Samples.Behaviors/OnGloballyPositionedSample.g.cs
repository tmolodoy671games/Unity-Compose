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
        protected override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }

        [Composable]
        protected override void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Preview());
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.FillMaxSize().Padding(100), content: static () =>
                {
                    var isSwitched = Remember(CurrentComposer.WithState((isSwitched, layout)).Remember<Action>(__ => () =>
                    {
                        var transitionSpec = Tween();
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: CurrentComposer.WithState(__.layout).Remember<Action>(__ => () =>
                        {
                            Box(CurrentComposer.WithState(__.layout).Remember<Action>(__ => () =>
                            {
                                Box(CurrentComposer.WithState(__.layout).Remember<Action>(__ => () =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnGloballyPositioned(CurrentComposer.WithState(__.layout).Remember<Action>(__ => it => layout.Value = it.GlobalCenter)));
                                }));
                            }));
                        }));
                    }));
                    var layout = Remember(CurrentComposer.WithState(isSwitched).Remember<Action>(__ => () => isSwitched.Value = !isSwitched.Value));
                    Box(modifier: Modifier.FillMaxSize(), content: () =>
                    {
                        var transitionSpec = Tween();
                        Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Size(40).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value), content: () =>
                        {
                            Box(() =>
                            {
                                Box(() =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20).OnGloballyPositioned(it => layout.Value = it.GlobalCenter));
                                });
                            });
                        });
                    });
                    Text(modifier: Modifier.Background(Color.blue).Padding(32).Border(32).OnClick(() => isSwitched.Value = !isSwitched.Value), color: Color.white, text: "Switch");
                    if (layout.Value.HasValue)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Size(10).Background(Color.red).Float().Position(left: measurer.GlobalToLocal(layout.Value.Value).x, top: measurer.GlobalToLocal(layout.Value.Value).y));
                    }
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }
}