// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class DisposableEffectSample : ComposeUI
    {
        protected override void Content()
        {
            Layout();
        }

        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Column(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .Name("launched-effect-disposal")
                    .FillMaxSize(),
                content: () =>
                {
                    var isEffectRunning = Remember(() => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        DisposableEffect(
                            null,
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
                        textColor: Color.white,
                        fontSize: 40,
                        modifier: Modifier
                            .Name("test-button")
                            .Background(isHovered.Value ? Color.cyan : Color.blue, Transition())
                            .NewPadding(vertical: 20)
                            .NewPadding(horizontal: isHovered.Value ? 40 : 20, transition: Transition())
                            .Border(radius: 16)
                            .NewMargin(top: 32)
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .OnClick(() => isEffectRunning.Value = !isEffectRunning.Value)
                    );
                }
            );
        }
    }
}