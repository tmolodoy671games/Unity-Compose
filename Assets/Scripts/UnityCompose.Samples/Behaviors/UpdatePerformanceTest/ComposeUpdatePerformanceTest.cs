// ReSharper disable ArrangeNamespaceBody

using StableCollections;

namespace UnityCompose.Samples.Behaviors.UpdatePerformanceTest
{
    internal partial class ComposeUpdatePerformanceTest : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            var parentSize = Remember(() => IMutableStableProperty.Create(Vector2.zero));
            Box(
                modifier: Modifier
                    .FillMaxSize()
                    .OnGloballyPositioned(it => parentSize.Value = it.SizeWithPaddings),
                content: () =>
                {
                    for (var i = 0; i < 1_00; i++)
                    {
                        var currentI = i;
                        Key(
                            key: currentI,
                            content: () =>
                            {
                                var position = Remember(static () => MutableStateOf(Vector2.zero));
                                LaunchedEffect(
                                    key: string.Empty,
                                    coroutine: () => PerformanceUtils.MoveRandomlyCoroutine(
                                        parentSize: () => parentSize.Value,
                                        it => position.Value = it
                                    )
                                );

                                var baseModifier = Remember(currentI, () => Modifier
                                    .Size(50)
                                    .Background(
                                        PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length])
                                    .Float()
                                );
                                Spacer(
                                    modifier:
                                    baseModifier
                                        .Position(
                                            left: position.Value.x,
                                            top: position.Value.y
                                        )
                                );
                            }
                        );
                    }
                }
            );
        }
    }
}