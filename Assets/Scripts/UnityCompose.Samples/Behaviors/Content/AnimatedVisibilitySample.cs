using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class AnimatedVisibilitySample : ComposeUI
    {
        private int Foo
        {
            [Composable]
            get => 1;
        }
        
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
            Box(
                contentAlignment: Alignment.Center,
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
                        enter: FadeIn(Tween(1_000)) + ExpandVertically(Tween(1_000)),
                        exit: FadeOut(Tween(1_000)) + ShrinkVertically(Tween(1_000)),
                        content: () => Text(
                            "Text",
                            fontSize: 64.Sp(),
                            modifier: Modifier
                                .Padding(16.Dp())
                                .Background(Color.lightBlue.ToSystemColor())
                        )
                    );
                }
            );
        }
    }
}