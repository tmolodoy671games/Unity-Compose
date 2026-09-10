// ReSharper disable ArrangeNamespaceBody

using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent : ComposeUI
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
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize()
                    .Background(Color.white),
                content: () =>
                {
                    var isHovered = Remember(() => MutableStateOf(false));
                    var isPressed = Remember(() => MutableStateOf(false));
                    var isCapturingPointer = Remember(() => MutableStateOf(false));
                    Box(
                        modifier: Modifier
                            .Padding(
                                horizontal: AnimateFloatAsState(
                                    isHovered.Value ? 160 : 40,
                                    animationSpec: Tween(duration: 2)
                                ).Value.Dp(),
                                vertical: 16.Dp()
                            )
                            .Background((isPressed.Value ? Color.darkBlue : Color.blue))
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .CapturePointer(isCapturingPointer.Value)
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Scale(2)
                            .OnLmbDown(() =>
                            {
                                isPressed.Value = true;
                                isCapturingPointer.Value = true;
                            })
                            .OnLmbUp(() =>
                            {
                                isPressed.Value = false;
                                isCapturingPointer.Value = false;
                            }),
                        content: () =>
                        {
                            CompositionLocalProvider(
                                LocalContentColor.Provides(Color.white),
                                content: () =>
                                {
                                    Text(
                                        text: "Click me",
                                        fontSize: 24.Sp(),
                                        modifier: Modifier
                                            .Scale((isPressed.Value ? 0.6f : 1f))
                                            .Alpha((isPressed.Value ? 0.6f : 1f))
                                    );
                                }
                            );
                        }
                    );
                }
            );
        }
    }

    partial interface IInterface
    {
        [Composable]
        int Foo(int a, int b);
    }

    partial class MyClass : IInterface
    {
        [Composable]
        public int Foo(int a, int b)
        {
            return 1;
        }
    }
}