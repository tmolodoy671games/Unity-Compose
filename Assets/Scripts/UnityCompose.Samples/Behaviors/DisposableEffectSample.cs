// ReSharper disable ArrangeNamespaceBody

using Compose.Net;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class DisposableEffectSample : ComposeUI
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
                horizontalAlignment: Alignment.CenterHorizontally,
                verticalArrangement: Arrangement.Center,
                modifier: Modifier
                    .TestTag("launched-effect-disposal")
                    .FillMaxSize(),
                content: () =>
                {
                    var isEffectRunning = Remember(() => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(
                            string.Empty,
                            it =>
                            {
                                Debug.Log("DisposableEffect()");
                                return it.OnDispose(() => Debug.Log("OnDispose()"));
                            }
                        );
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(() => MutableStateOf(false));
                    Text(
                        text: $"DisposableEffect is {onOrOff}",
                        color: Color.white.ToSystemColor(),
                        fontSize: 40.Sp(),
                        modifier: Modifier
                            .TestTag("test-button")
                            .Background(isHovered.Value ? Color.cyan.ToSystemColor() : Color.blue.ToSystemColor())
                            .Padding(vertical: 20.Dp())
                            .Padding(horizontal: isHovered.Value ? 40.Dp() : 20.Dp())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Margin(top: 32.Dp())
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .OnClick(() => isEffectRunning.Value = !isEffectRunning.Value)
                    );
                }
            );
        }
    }
}