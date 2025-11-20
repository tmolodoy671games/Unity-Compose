// ReSharper disable ArrangeNamespaceBody

using System;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview()
        {
            Spacer(Modifier);
        }

        [Composable]
        private static void EmptyColumn([Composable] Action action)
        {
            action();
        }

        [Composable]
        private static void EmptySpacer(IModifier modifier)
        {
        }

        [Composable]
        private static void Layout()
        {
            // var value1 = Remember(static () => MutableStateOf(false));
            // var value2 = Remember(static () => MutableStateOf(0));
            // var value3 = Remember(static () => MutableStateOf(1.2));
            // var value4 = Remember(static () => MutableStateOf("text"));

            Spacer(Modifier);
            // EmptySpacer(Modifier);

            // EmptyColumn(() =>
            // {
            //     EmptySpacer(Modifier);
            //     EmptySpacer(Modifier);
            // });
            //
            // EmptyColumn(() =>
            // {
            //     EmptySpacer(Modifier);
            //     EmptySpacer(Modifier);
            // });

            // Spacer(
            //     modifier: Modifier
            //         .Size(100)
            //         .Background(Color.cyan)
            //         .Border(radius: 32)
            // );

            // var isHovered = Remember(() => MutableStateOf(false));
            // var isPressed = Remember(() => MutableStateOf(false));
            // Box(
            //     modifier: Modifier
            //         .FillMaxSize(),
            //     horizontalAlignment: Alignment.Horizontal.Center,
            //     verticalAlignment: Alignment.Vertical.Center,
            //     content: () =>
            //     {
            //         Spacer(
            //             modifier: Modifier
            //                 .Size(100)
            //                 .Background(isPressed.Value ? Color.cyan : Color.blue, Transition())
            //                 .Border(radius: 32)
            //                 .Scale(isHovered.Value ? 2 : 1, transition: Transition())
            //                 .OnMouseEnter(() => isHovered.Value = true)
            //                 .OnMouseLeave(() =>
            //                 {
            //                     isPressed.Value = false;
            //                     isHovered.Value = false;
            //                 })
            //                 .OnMouseDown(() => isPressed.Value = true)
            //                 .OnMouseUp(() => isPressed.Value = false)
            //         );
            //     }
            // );
        }
    }
}