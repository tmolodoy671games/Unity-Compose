using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Compose.Net;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Stability
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
        public static void LaunchedEffect<TKey>(
            TKey key,
            Func<CancellationToken, Task> coroutine
        )
        {
            var _ = Remember(key, () =>
            {
                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                coroutine(token);
                return new CustomComposeDisposable(() =>
                {
                    tokenSource.Cancel();
                    tokenSource.Dispose();
                });
            });
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
                    var count = Remember(() => MutableStateOf(0));
                    Text(
                        text: count.Value.ToString(),
                        color: Color.white.ToSystemColor(),
                        fontSize: 40.Sp(),
                        modifier: Modifier
                            .TestTag("test-label")
                            .Background(Color.red.ToSystemColor())
                            .Padding(all: 10.Dp())
                    );
                    var isEffectRunning = Remember(() => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        LaunchedEffect(
                            key: "",
                            coroutine: async it =>
                            {
                                while (true)
                                {
                                    await Task.Delay(1_000, it);
                                    count.Value++;
                                }
                            }
                        );
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = Remember(() => MutableStateOf(false));
                    Text(
                        text: $"Launched Effect is {onOrOff}",
                        color: Color.white.ToSystemColor(),
                        fontSize: 40.Sp(),
                        modifier: Modifier
                            .TestTag("test-button")
                            .Padding(top: 32.Dp())
                            .Background(isHovered.Value
                                ? Color.cyan.ToSystemColor()
                                : Color.blue.ToSystemColor())
                            .Padding(vertical: 20.Dp())
                            .Padding(horizontal: isHovered.Value ? 40.Dp() : 20.Dp())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .OnClick(() => isEffectRunning.Value = !isEffectRunning.Value)
                    );
                }
            );
        }
    }
}