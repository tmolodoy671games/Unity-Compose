using System;
using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters;

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
                    );
                }
            );
        }
    }
}