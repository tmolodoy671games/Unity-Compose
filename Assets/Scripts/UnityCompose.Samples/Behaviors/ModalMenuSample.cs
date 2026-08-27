// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ModalMenuSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            var showModalMenu = Remember(() => MutableStateOf(false));
            if (showModalMenu.Value)
            {
                ModalMenu(() =>
                {
                    Box(
                        alignment: Alignment.Center,
                        modifier: Modifier.FillMaxSize()
                            .Background(Color.black.With(a: 0.9f)),
                        content: () =>
                            Spacer(
                                modifier: Modifier
                                    .Background(Color.lightYellow)
                                    .Size(100.Px())
                                    .Border(16.Px())
                                    .OnClick(() => showModalMenu.Value = false)
                            )
                    );
                });
            }

            Box(
                alignment: Alignment.Center,
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    Box(
                        modifier: Modifier
                            .Padding(horizontal: 20.Px(), vertical: 8.Px())
                            .Background(Color.lightGreen)
                            .Blur(AnimateFloatAsState(LocalModalMenuVisibility.Current.ToInt()).Value * 10)
                            .OnClick(() => showModalMenu.Value = true)
                            .Border(16.Px()),
                        content: () =>
                        {
                            Text(
                                text: "Show modal",
                                color: Color.white,
                                fontSize: 32
                            );
                        }
                    );
                }
            );
        }

        [Composable]
        protected override void Preview()
        {
            Content();
        }
    }
}