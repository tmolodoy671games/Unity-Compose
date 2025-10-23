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
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Box(
                        modifier: Modifier
                            .Background(Color.red)
                            .Size(400),
                        content: () =>
                        {
                            Spacer(
                                modifier: Modifier
                                    .Size(100)
                                    .Float()
                                    .Background(Color.yellow)
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
            //                 .Background(Color.darkRed)
            //                 .Size(400),
            //             content: () =>
            //             {
            //                 Spacer(
            //                     style: ComposeStyle.Empty
            //                         .Size(100)
            //                         .Position(Position.Absolute)
            //                         .Background(Color.greenYellow)
            //                 );
            //             }
            //         );
            //     }
            // );
        }
    }
}