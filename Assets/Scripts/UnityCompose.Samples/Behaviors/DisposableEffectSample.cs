using UnityEngine.UIElements;

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
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier
                    .Name("launched-effect-disposal")
                    .FlexGrow(1),
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
                    Label(
                        text: $"DisposableEffect is {onOrOff}",
                        textColor: Color.white,
                        fontSize: 40,
                        style: Modifier
                            .Name("test-button")
                            .BackgroundColor(isHovered.Value ? Color.cyan : Color.blue, Transition())
                            .PaddingVertical(20)
                            .PaddingHorizontal(isHovered.Value ? 40 : 20, Transition())
                            .BorderRadius(16)
                            .MarginTop(32)
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .OnClick(() => isEffectRunning.Value = !isEffectRunning.Value)
                    );
                }
            );
        }
    }
}