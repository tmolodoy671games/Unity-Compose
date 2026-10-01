using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class AnimatedVisibilitySample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            Box(
                alignment: Alignment.Center,
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    var isVisible = Remember(() => MutableStateOf(true));
                    Spacer(
                        Modifier
                            .Size(100.Dp(), 100.Dp())
                            .Background(Color.forestGreen.ToSystemColor())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnClick(() => isVisible.Value = !isVisible.Value)
                    );
                    AnimatedVisibility(
                        visible: isVisible.Value,
                        enter: FadeIn(Tween(1)) + ExpandVertically(Tween(1)),
                        exit: FadeOut(Tween(1)) + ShrinkVertically(Tween(1)),
                        content: () => Text(
                            "Text",
                            fontSize: 64.Sp(),
                            modifier: Modifier
                                .Background(Color.lightBlue.ToSystemColor())
                                .Margin(16.Dp())
                        )
                    );
                }
            );
        }
    }
}