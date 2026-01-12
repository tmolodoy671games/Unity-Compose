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
            var parentCoordinates = Remember(() => MutableStateOf(Optional.Empty<LayoutCoordinates>()));
            Column(
                horizontalAlignment: Alignment.CenterHorizontally,
                modifier: Modifier.FillMaxSize()
                    .Padding(100.Px())
                    .OnGloballyPositioned(it => parentCoordinates.Value = it),
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
                                alignment: Alignment.Center,
                                modifier: Modifier
                                    .Size(40.Px())
                                    .Background(Color.blue)
                                    .Offset(
                                        x: AnimateFloatAsState(
                                            targetValue: 500 * isSwitched.Value.ToInt(),
                                            animationSpec: transitionSpec
                                        ).Value.Px()
                                    ),
                                content: () =>
                                {
                                    Box(() =>
                                    {
                                        Box(() =>
                                        {
                                            Spacer(
                                                Modifier
                                                    .Background(Color.green)
                                                    .Size(20.Px())
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
                            .Padding(32.Px())
                            .Border(32.Px())
                            .OnClick(() => isSwitched.Value = !isSwitched.Value),
                        color: Color.white,
                        text: "Switch"
                    );

                    if (layout.Value.HasValue && parentCoordinates.Value.HasValue)
                    {
                        var parentCoordinatesValue = parentCoordinates.Value.Value;
                        Spacer(
                            modifier: Modifier
                                .Size(10.Px())
                                .Background(Color.red)
                                .Float()
                                .Position(
                                    left: parentCoordinatesValue.GlobalToLocal(layout.Value.Value).x.Px(),
                                    top: parentCoordinatesValue.GlobalToLocal(layout.Value.Value).y.Px()
                                )
                        );
                    }
                }
            );
        }
    }
}