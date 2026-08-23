// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.UpdatePerformanceTest
{
    internal partial class ComposeUpdatePerformanceTest : ComposeUI
    {
        [SerializeField] private SlotTableType type;

        protected override SlotTableType SlotTableType => type;

        [Composable]
        protected override void Content()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            var parentSize = Remember(() => MutableStateOf(Vector2.zero));
            Box(
                modifier: Modifier
                    .FillMaxSize()
                    .OnGloballyPositioned(it => parentSize.Value = it.Size),
                content: () =>
                {
                    for (var i = 0; i < 1_000; i++)
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

        [Composable]
        private static void Item(int currentI, Vector2 parentSize)
        {
            var position = Remember(static () => MutableStateOf(Vector2.zero));
            LaunchedEffect(
                key: parentSize,
                coroutine: () => PerformanceUtils.MoveRandomlyCoroutine(
                    parentSize: () => parentSize,
                    it => position.Value = it
                )
            );

            var baseModifier = Remember(currentI, () => Modifier
                .Size(50.Px())
                .Background(
                    PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length])
                .Float()
            );
            Spacer(
                modifier:
                baseModifier
                    .Position(
                        left: position.Value.x.Px(),
                        top: position.Value.y.Px()
                    )
            );
        }
    }
}