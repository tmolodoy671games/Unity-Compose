// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositionedSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Layout();
        }

        [Composable]
        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Column(
                horizontalAlignment: Alignment.Horizontal.Center,
                modifier: Modifier.FillMaxSize()
                    .Padding(100),
                content: () =>
                {
                    var isSwitched = Remember(static () => MutableStateOf(false));
                    var layout = Remember(static () => MutableStateOf(Optional.Empty<Vector2>()));
                    Box(
                        modifier: Modifier.FillMaxSize(),
                        content: () =>
                        {
                            var transitionSpec = Tween();
                            Box(
                                horizontalAlignment: Alignment.Horizontal.Center,
                                verticalAlignment: Alignment.Vertical.Center,
                                modifier: Modifier
                                    .Size(40)
                                    .Background(Color.blue)
                                    .Offset(x: AnimateFloatAsState(
                                        targetValue: 500 * isSwitched.Value.ToInt(),
                                        animationSpec: transitionSpec
                                    ).Value),
                                content: () =>
                                {
                                    Box(() =>
                                    {
                                        Box(() =>
                                        {
                                            Spacer(
                                                Modifier
                                                    .Background(Color.green)
                                                    .Size(20)
                                                    .OnGloballyPositioned(it => layout.Value = it.GlobalCenter)
                                            );
                                        });
                                    });
                                }
                            );
                        }
                    );
                    Text(
                        modifier: Modifier
                            .Background(Color.blue)
                            .Padding(32)
                            .Border(32)
                            .OnClick(() => isSwitched.Value = !isSwitched.Value),
                        color: Color.white,
                        text: "Switch"
                    );

                    if (layout.Value.HasValue)
                    {
                        // var measurer = LocalLayoutMeasurer.Current;
                        // Spacer(
                        //     modifier: Modifier
                        //         .Size(10)
                        //         .Background(Color.red)
                        //         .Float()
                        //         .Position(
                        //             left: measurer.GlobalToLocal(layout.Value.Value).x,
                        //             top: measurer.GlobalToLocal(layout.Value.Value).y
                        //         )
                        // );
                    }
                }
            );
        }
    }
}