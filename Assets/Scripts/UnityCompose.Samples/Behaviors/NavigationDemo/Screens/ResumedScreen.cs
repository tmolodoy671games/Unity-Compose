using System;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class ResumedScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        var coordinator = FindCoordinator<ISampleCoordinator>();
        Box(
            alignment: Alignment.Center,
            modifier: modifier
                .FillMaxSize()
                .Background(Color.green)
                .OnClick(coordinator.ShowPausedScreen),
            content: () =>
            {
                var showMenu = Remember(() => MutableStateOf(false));
                DropdownMenu(
                    expanded: showMenu.Value,
                    onDismissRequest: () => showMenu.Value = false
                );
                Spacer(
                    modifier: Modifier
                        .Size(300.Px())
                        .Background(Color.blue)
                        .OnClick(() => showMenu.Value = true)
                        .Scale(1 - 0.5f * (1 - LocalTransitionProgress.Current))
                );
            }
        );
    }

    [Composable]
    private static void DropdownMenu(
        bool expanded,
        Action onDismissRequest
    )
    {
        // if (expanded)
        // {
        //     ModalMenu(() =>
        //     {
        //         Box(
        //             alignment: Alignment.Center,
        //             modifier: Modifier
        //                 .FillMaxSize()
        //                 .Background(Color.black.With(a: 0.9f)),
        //             content: () =>
        //             {
        //                 Spacer(
        //                     Modifier
        //                         .Size(100.Px())
        //                         .Background(Color.yellow)
        //                         .OnClick(onDismissRequest)
        //                 );
        //             }
        //         );
        //     });
        // }
    }
}