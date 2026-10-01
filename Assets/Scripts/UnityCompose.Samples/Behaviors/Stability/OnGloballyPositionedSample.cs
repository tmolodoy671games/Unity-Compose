// ReSharper disable ArrangeNamespaceBody

using Compose.Net;
using SharpExtensions;

namespace UnityCompose.Samples.Behaviors.Stability
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
                    .Padding(all: 100.Dp())
                    .OnGloballyPositioned(it => parentCoordinates.Value = it),
                content: () =>
                {
                    var isSwitched = Remember(static () => MutableStateOf(false));
                    var layout = Remember(static () => MutableStateOf(Optional.Empty<Vector2>()));
                    Box(
                        modifier: Modifier.FillMaxSize(),
                        content: () =>
                        {
                            var transitionSpec = Tween(1_000);
                            Box(
                                alignment: Alignment.Center,
                                modifier: Modifier
                                    .Size(40.Dp())
                                    .Background(Color.blue.ToSystemColor())
                                    .Offset(
                                        x: AnimateFloatAsState(
                                            targetValue: 500 * isSwitched.Value.ToInt(),
                                            animationSpec: transitionSpec
                                        ).Value.Dp()
                                    ),
                                content: () =>
                                {
                                    Box(() =>
                                    {
                                        Box(() =>
                                        {
                                            Spacer(
                                                Modifier
                                                    .Background(Color.green.ToSystemColor())
                                                    .Size(20.Dp())
                                                    .OnGloballyPositioned(it => layout.Value = it.GlobalCenter.ToVector2())
                                            );
                                        });
                                    });
                                }
                            );
                        }
                    );
                    Text(
                        modifier: Modifier
                            .Background(Color.blue.ToSystemColor())
                            .Padding(all: 32.Dp())
                            .Clip(RoundedCornerShape(32.Dp()))
                            .OnClick(() => isSwitched.Value = !isSwitched.Value),
                        color: Color.white.ToSystemColor(),
                        text: "Switch"
                    );

                    if (layout.Value.HasValue && parentCoordinates.Value.HasValue)
                    {
                        var parentCoordinatesValue = parentCoordinates.Value.Value;
                        Spacer(
                            modifier: Modifier
                                .Size(10.Dp())
                                .Background(Color.red.ToSystemColor())
                                .Float()
                                .Position(
                                    left: parentCoordinatesValue.GlobalToLocal(layout.Value.Value.ToOffset()).X.Dp(),
                                    top: parentCoordinatesValue.GlobalToLocal(layout.Value.Value.ToOffset()).Y.Dp()
                                )
                        );
                    }
                }
            );
        }
    }
}