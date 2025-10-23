using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action<global::UnityCompose.IBoxScope>>(null, scope =>
                {
                    Box(modifier: Modifier.Background(Color.red).Size(400), content: RememberComposable<global::System.Action>(scope, () =>
                    {
                        Spacer(modifier: Modifier.Size(100).Float().Background(Color.yellow).Then(scope.Position(top: 5)));
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
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}