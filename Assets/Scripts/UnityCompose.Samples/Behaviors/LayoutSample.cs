using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LayoutSample : ComposeUI
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
            Box(
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: IModifier.Empty
                    .Width(100.Percent())
                    .Height(100.Percent()),
                content: () =>
                {
                    Box(
                        style: IModifier.Empty
                            .BackgroundColor(Color.red)
                            .Size(400),
                        content: () =>
                        {
                            Spacer(
                                style: IModifier.Empty
                                    .Size(100)
                                    .Position(Position.Absolute)
                                    .BackgroundColor(Color.yellow)
                                    .Top(5)
                            );
                        }
                    );
                }
            );

            // Box(
            //     alignHorizontally: Align.Center,
            //     alignVertically: Justify.Center,
            //     style: ComposeStyle.Empty
            //         .Width(100.Percent())
            //         .Height(100.Percent()),
            //     content: () =>
            //     {
            //         Box(
            //             alignHorizontally: Align.Center,
            //             alignVertically: Justify.Center,
            //             style: ComposeStyle.Empty
            //                 .BackgroundColor(Color.darkRed)
            //                 .Size(400),
            //             content: () =>
            //             {
            //                 Spacer(
            //                     style: ComposeStyle.Empty
            //                         .Size(100)
            //                         .Position(Position.Absolute)
            //                         .BackgroundColor(Color.greenYellow)
            //                 );
            //             }
            //         );
            //     }
            // );
        }
    }
}