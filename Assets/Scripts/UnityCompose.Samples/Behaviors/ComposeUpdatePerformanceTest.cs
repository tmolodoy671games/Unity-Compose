// ReSharper disable ArrangeNamespaceBody

using Compose.Net;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ComposeUpdatePerformanceTest : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            var parentSize = Remember(() => MutableStateOf(Vector2.zero));
            Box(
                modifier: Modifier
                    .FillMaxSize()
                    .OnGloballyPositioned(it => parentSize.Value = it.Size.ToVector2()),
                content: () =>
                {
                    for (var i = 0; i < 1; i++)
                    {
                        var currentI = i;
                        Key(
                            key: currentI,
                            content: () => Item(currentI, parentSize.Value)
                        );
                    }
                }
            );
        }

        [BoxScope]
        [Composable]
        private static void Item(int currentI, Vector2 parentSize)
        {
            var position = Remember(static () => MutableStateOf(Vector2.zero));
            if (parentSize.x > 0)
            {
                LaunchedEffect(
                    key: 1,
                    coroutine: () => PerformanceUtils.MoveRandomlyCoroutine(
                        parentSize: () => parentSize,
                        it =>
                        {
                            position.Value = it;
                        })
                );
            }

            var baseModifier = Remember(currentI, [BoxScope]() => Modifier
                .Size(50.Dp())
                .Background(
                    PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length].ToSystemColor()
                )
                .Float()
            );
            Spacer(
                modifier:
                baseModifier
                    .Position(
                        left: position.Value.x.Dp(),
                        top: position.Value.y.Dp()
                    )
            );
        }
    }
}