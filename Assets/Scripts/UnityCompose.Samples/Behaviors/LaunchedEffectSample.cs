using System.Collections;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LaunchedEffectSample : ComposeUI
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
                    var count = Remember(() => MutableStateOf(0));
                    Label(
                        text: count.Value.ToString(),
                        textColor: Color.white,
                        fontSize: 40,
                        style: Modifier
                            .Name("test-label")
                            .Background(Color.red)
                            .Padding(10)
                    );
                    var isEffectRunning = Remember(() => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        IEnumerator EffectCoroutine()
                        {
                            while (true)
                            {
                                yield return new WaitForSeconds(1f);
                                count.Value++;
                            }
                        }

                        LaunchedEffect(
                            key: null,
                            EffectCoroutine()
                        );
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(() => MutableStateOf(false));
                    Label(
                        text: $"Launched Effect is {onOrOff}",
                        textColor: Color.white,
                        fontSize: 40,
                        style: Modifier
                            .Name("test-button")
                            .Background(isHovered.Value ? Color.cyan : Color.blue, Transition())
                            .PaddingVertical(20)
                            .PaddingHorizontal(isHovered.Value ? 40 : 20, Transition())
                            .BorderRadius(16)
                            .Margin(top: 32)
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .OnClick(() => isEffectRunning.Value = !isEffectRunning.Value)
                    );
                }
            );
        }
    }
}