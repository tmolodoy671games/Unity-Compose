using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class LayoutSample
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Box(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: IModifier.Empty.Width(100.Percent()).Height(100.Percent()), content: RememberComposable<global::System.Action>(null, () =>
                {
                    Box(style: IModifier.Empty.BackgroundColor(Color.red).Size(400), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        Spacer(style: IModifier.Empty.Size(100).Position(Position.Absolute).BackgroundColor(Color.yellow).Top(5));
                    }));
                }));
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
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}