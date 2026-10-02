using System;
using Compose.Net;
using Compose.Net.Internals.Entities.Path;
using SharpExtensions;
using UnityEngine.UIElements;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class ButtonSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            Column(
                verticalArrangement: Arrangement.Center,
                horizontalAlignment: Alignment.CenterHorizontally,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    Spacer(
                        Modifier
                            .Height(200.Dp())
                            .Width(AnimateFloatAsState(isHovered.Value ? 600 : 400).Value.Dp())
                            .Background(Color.forestGreen.ToSystemColor())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnPointerEnter(() => isHovered.Value = true)
                            .OnPointerLeave(() => isHovered.Value = false)
                            .DrawOn(it =>
                            {
                                it.DrawLine(
                                    color: Color.black.ToSystemColor(),
                                    start: new Offset(10, 10),
                                    end: new Offset(100, 100),
                                    strokeCap: StrokeCap.Round,
                                    strokeWidth: 1
                                );
                                it.DrawRect(
                                    color: Color.white.ToSystemColor(),
                                    topLeft: new Offset(10, 10),
                                    size: new FloatSize(40, 40)
                                );
                                it.DrawRoundRect(
                                    color: Color.wheat.ToSystemColor(),
                                    topLeft: new Offset(30, 30),
                                    size: new FloatSize(80, 40),
                                    cornerRadius: 16
                                );
                                var path = Path()
                                    .MoveTo(new Offset(20, 20))
                                    .LineTo(new Offset(100, 20))
                                    .LineTo(new Offset(60, 80))
                                    .Close();

                                it.DrawPath(
                                    path: path,
                                    color: Color.black.ToSystemColor()
                                );
                            })
                    );
                }
            );
        }
    }
}