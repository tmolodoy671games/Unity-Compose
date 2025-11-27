// ReSharper disable ArrangeNamespaceBody

using System.Collections;
using StableCollections;

namespace UnityCompose.Samples.Behaviors.UpdatePerformanceTest
{
    internal partial class ComposeUpdatePerformanceTest : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            var parentSize = Remember(() => IMutableStableProperty.Create(Vector2.zero));
            Box(
                modifier: Modifier
                    .FillMaxSize()
                    .OnGloballyPositioned(it => parentSize.Value = it.SizeWithPaddings),
                content: () =>
                {
                    for (var i = 0; i < 1_000; i++)
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

                                Spacer(
                                    modifier: Modifier
                                        .Size(50)
                                        .Background(
                                            PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length])
                                        .Float()
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