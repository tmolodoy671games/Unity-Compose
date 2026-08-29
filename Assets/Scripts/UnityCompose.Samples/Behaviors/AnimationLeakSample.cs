// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent]
    internal partial class AnimationLeakSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            Column(
                horizontalAlignment: Alignment.CenterHorizontally,
                verticalArrangement: Arrangement.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    var showMovingSquare = Remember(() => MutableStateOf(true));
                    if (showMovingSquare.Value)
                    {
                        var isSwitched = Remember(() => MutableStateOf(false));
                        var offset = AnimateFloatAsState(
                            targetValue: isSwitched.Value ? 100 : -100,
                            animationSpec: Tween(duration: 3)
                        ).Value;
                        Box(() =>
                            Box(() =>
                                Spacer(
                                    Modifier
                                        .Size(100.Dp())
                                        .Background(Color.green)
                                        .Offset(offset.Dp())
                                        .OnClick(() => isSwitched.Value = !isSwitched.Value)
                                )
                            )
                        );
                    }

                    var isHovered = Remember(() => MutableStateOf(false));
                    Text(
                        text: "Switch",
                        color: Color.white,
                        fontSize: 32.Sp(),
                        modifier: Modifier
                            .Background(Color.blue)
                            .Padding(
                                horizontal: 32.Dp() + 32 * AnimateFloatAsState(isHovered.Value.ToInt()).Value.Dp(),
                                vertical: 16.Dp()
                            )
                            .Border(16.Dp())
                            .Margin(top: 32.Dp())
                            .OnClick(() => showMovingSquare.Value = !showMovingSquare.Value)
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                    );
                }
            );
        }
    }
}