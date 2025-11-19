// ReSharper disable ArrangeNamespaceBody

using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ComposePerformanceTest : ComposeUI
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
                    for (var i = 0; i < 1000; i++)
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
                                        .Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length])
                                        .Float()
                                        .Position(
                                            left: position.Value.x,
                                            top: position.Value.y
                                        )
                                );
                            }
                        );
                    }

                    var fps = Remember(() => MutableStateOf(0));
                    LaunchedEffect(string.Empty, () => PerformanceUtils.MeasureFpsCoroutine(it => fps.Value = it));

                    Text(
                        text: fps.Value.ToString(),
                        color: Color.white,
                        modifier: Modifier
                            .Float()
                            .Background(Color.black)
                            .Position(
                                right: 40,
                                top: 40
                            )
                    );
                }
            );
        }
    }
}