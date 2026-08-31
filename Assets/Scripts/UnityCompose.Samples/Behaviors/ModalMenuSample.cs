// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;
using StableCollections;

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
                                    .Size(100.Dp())
                                    .Clip(RoundedCornerShape(16.Dp()))
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
                            .Padding(horizontal: 20.Dp(), vertical: 8.Dp())
                            .Background(Color.lightGreen)
                            .Blur(AnimateFloatAsState(LocalModalMenuTags.Current.IsNotEmpty().ToInt()).Value * 10)
                            .OnClick(() => showModalMenu.Value = true)
                            .Clip(RoundedCornerShape(16.Dp())),
                        content: () =>
                        {
                            Text(
                                text: "Show modal",
                                color: Color.white,
                                fontSize: 32.Sp()
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