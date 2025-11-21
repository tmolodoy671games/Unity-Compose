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
                var columnModifier = Modifier
                    .Margin(vertical: 4);
                var spacerModifier = Modifier
                    .Size(10)
                    .Background(Color.white)
                    .Margin(horizontal: 4);
                var time = TimeUtils.Measure(() =>
                {
                    for (var i = 0; i < 40_000; i++)
                    {
                        CurrentComposer.BeginComposeGroup(0);
                        CurrentComposer.EndComposeGroup(static () => {});
                        // var a = Remember(static () => 1);
                    }
                    
                    // for (var i = 0; i < 100; i++)
                    // {
                    //     Column(
                    //         modifier: columnModifier,
                    //         content: () =>
                    //         {
                    //             for (var j = 0; j < 100; j++)
                    //             {
                    //                 Spacer(
                    //                     modifier: spacerModifier
                    //                 );
                    //             }
                    //         }
                    //     );
                    // }
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