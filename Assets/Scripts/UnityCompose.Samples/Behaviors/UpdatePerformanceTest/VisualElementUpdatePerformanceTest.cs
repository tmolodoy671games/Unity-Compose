// ReSharper disable ArrangeNamespaceBody

using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.UpdatePerformanceTest
{
    internal class VisualElementUpdatePerformanceTest : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            var root = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>();
            for (var i = 0; i < 1_000; i++)
            {
                var childElement = new VisualElement
                {
                    style =
                    {
                        backgroundColor = PerformanceUtils.Colors[i % PerformanceUtils.Colors.Length],
                        width = 50,
                        height = 50,
                        position = Position.Absolute
                    }
                };
                root.Add(childElement);

                StartCoroutine(
                    PerformanceUtils.MoveRandomlyCoroutine(
                        () => root.layout.size,
                        it =>
                        {
                            childElement.style.left = it.x;
                            childElement.style.top = it.y;
                        }
                    )
                );
            }
        }
    }
}