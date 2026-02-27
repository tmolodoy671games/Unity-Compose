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
                    var circleColor = AnimateColorAsState(
                        isPressed.Value ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f),
                        Tween(1)
                    ).Value;
                    Box(
                        modifier: Modifier
                            .Padding(
                                horizontal: AnimateFloatAsState(isHovered.Value ? 80 : 40).Value.Px(),
                                vertical: 16.Px()
                            )
                            .Background(AnimateColorAsState(isPressed.Value ? Color.darkBlue : Color.blue).Value)
                            .Border(radius: 16.Px())
                            .OnMouseEnter(() => isHovered.Value = true)
                            .OnMouseLeave(() => isHovered.Value = false)
                            .CapturePointer(isCapturingPointer.Value)
                            .DrawAfter(it =>
                            {
                                var size = it.visualElement.layout.size;
                                it.painter2D.fillColor = circleColor;
                                it.painter2D.BeginPath();
                                it.painter2D.Arc(size / 2, 50, 0, 360);
                                it.painter2D.Fill();
                            })
                            .Clip()
                            .Scale(2)
                            .OnLmbClick(() => Debug.Log("LMB"))
                            .OnRmbClick(() => Debug.Log("RMB"))
                            .OnMmbClick(() => Debug.Log("MMB"))
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
                                        fontSize: 24,
                                        modifier: Modifier
                                            .Scale(AnimateFloatAsState(isPressed.Value ? 0.6f : 1f).Value)
                                            .Alpha(AnimateFloatAsState(isPressed.Value ? 0.6f : 1f).Value)
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