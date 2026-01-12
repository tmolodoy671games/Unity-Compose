using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.BuildUpPerformanceTest
{
    [DisallowMultipleComponent, HideMonoScript]
    internal partial class ComposeBuildUpPerformanceTest : ComposeUI
    {
        [Button]
        private void Test()
        {
            var root = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>();

            root.SetContent(static () => {});
            root.SetContent(() =>
            {
                var rowModifier = Modifier
                    .Margin(vertical: 2.Px());
                var spacerModifier = Modifier
                    .Size(4.Px())
                    .Background(Color.white)
                    .Margin(horizontal: 2.Px());
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