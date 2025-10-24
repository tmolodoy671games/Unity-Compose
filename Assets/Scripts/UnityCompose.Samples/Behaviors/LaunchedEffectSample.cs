using System.Collections;

// ReSharper disable ArrangeNamespaceBody

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
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .Name("launched-effect-disposal")
                    .FillMaxSize(),
                content: () =>
                {
                    var count = Remember(() => MutableStateOf(0));
                    Text(
                        text: count.Value.ToString(),
                        color: Color.white,
                        fontSize: 40,
                        modifier: Modifier
                            .Name("test-label")
                            .Background(Color.red)
                            .Padding(all: 10)
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
                    Text(
                        text: $"Launched Effect is {onOrOff}",
                        color: Color.white,
                        fontSize: 40,
                        modifier: Modifier
                            .Name("test-button")
                            .Background(isHovered.Value ? Color.cyan : Color.blue, Transition())
                            .Padding(vertical: 20)
                            .Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition())
                            .Border(radius: 16)
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