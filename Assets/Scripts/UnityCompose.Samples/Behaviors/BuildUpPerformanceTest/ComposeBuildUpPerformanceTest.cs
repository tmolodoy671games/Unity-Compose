using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.BuildUpPerformanceTest
{
    [DisallowMultipleComponent]
    internal partial class ComposeBuildUpPerformanceTest : ComposeUI
    {
        [SerializeField] private SlotTableType type;

        protected override SlotTableType SlotTableType => type;

        [Button]
        private void Test()
        {
            var root = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>();
            root.SetContent(static (_, _) => {});
            root.SetContent((_, _) =>
            {
                var rowModifier = Modifier
                    .Margin(vertical: 2.Dp());
                var spacerModifier = Modifier
                    .Size(4.Dp())
                    .Background(Color.white)
                    .Margin(horizontal: 2.Dp());
                GC.Collect();
                var time = TimeUtils.Measure(() =>
                {
                    // for (var i = 0; i < 1_000_000; i++)
                    // {
                    //     CurrentComposer.BeginComposeGroup(i, true);
                    //     CurrentComposer.EndComposeGroup(static () => { }); 
                    // }
                    
                    for (var i = 0; i < 100; i++)
                    {
                        Row(
                            modifier: rowModifier,
                            content: () =>
                            {
                                for (var j = 0; j < 100; j++)
                                {
                                    Spacer(
                                        modifier: spacerModifier
                                    );
                                }
                            }
                        );
                    }
                });

                Debug.Log(time.TotalSeconds.ToString("F3"));
            });
        }

        [Composable]
        protected override void Content()
        {
        }
    }
}