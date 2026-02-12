using SharpExtensions;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.BuildUpPerformanceTest
{
    [DisallowMultipleComponent]
    internal class VisualElementBuildUpPerformanceTest : MonoBehaviour
    {
        [Button]
        private void Test()
        {
            var root = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>();
            root.Clear();

            var time = TimeUtils.Measure(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    var column = new VisualElement
                    {
                        style =
                        {
                            flexDirection = FlexDirection.Row,
                            marginTop = 2,
                            marginBottom = 2
                        }
                    };
                    root.Add(column);

                    for (var j = 0; j < 100; j++)
                    {
                        var spacer = new VisualElement
                        {
                            style =
                            {
                                width = 4,
                                height = 4,
                                backgroundColor = Color.white,
                                marginLeft = 2,
                                marginRight = 2
                            }
                        };
                        column.Add(spacer);
                    }
                }
            });
            
            Debug.Log(time.TotalSeconds.ToString("F3"));
            
        }
    }
}