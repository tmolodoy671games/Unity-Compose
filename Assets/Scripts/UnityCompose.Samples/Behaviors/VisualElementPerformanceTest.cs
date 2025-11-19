// ReSharper disable ArrangeNamespaceBody

using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal class VisualElementPerformanceTest : MonoBehaviour
    {
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>();
            for (var i = 0; i < 1000; i++)
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

            var fpsLabel = new Label()
            {
                style =
                {
                    backgroundColor = Color.black,
                    color = Color.white,
                    paddingTop = 10,
                    paddingBottom = 10,
                    paddingLeft = 10,
                    paddingRight = 10,
                    position = Position.Absolute,
                    right = 40,
                    top = 40
                }
            };
            root.Add(fpsLabel);

            StartCoroutine(PerformanceUtils.MeasureFpsCoroutine(it => fpsLabel.text = it.ToString()));
        }
    }
}