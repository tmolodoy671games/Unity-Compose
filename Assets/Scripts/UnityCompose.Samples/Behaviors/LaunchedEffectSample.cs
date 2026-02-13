using System;
using System.Collections;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LaunchedEffectSample : ComposeUI
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
                            .Padding(all: 10.Px())
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
                            key: string.Empty,
                            coroutine: () => EffectCoroutine()
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
                            .Background(AnimateColorAsState(isHovered.Value ? Color.cyan : Color.blue).Value)
                            .Padding(vertical: 20.Px())
                            .Padding(horizontal: AnimateFloatAsState(isHovered.Value ? 40 : 20).Value.Px())
                            .Border(radius: 16.Px())
                            .Margin(top: 32.Px())
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .OnClick(() => isEffectRunning.Value = !isEffectRunning.Value)
                    );
                }
            );
        }
    }
}