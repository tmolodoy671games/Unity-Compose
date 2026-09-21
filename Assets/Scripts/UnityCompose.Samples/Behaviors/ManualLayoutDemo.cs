using System;
using SharpExtensions;
// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent]
    internal partial class ManualLayoutDemo : MonoBehaviour
    {
        private static readonly IMutableState<bool> UpdateState = MutableStateOf(false);
        private static readonly IMutableState<bool> SwitchState = MutableStateOf(false);
        private static readonly IMutableState<int> AddState = MutableStateOf(0);
        private static readonly ICompositionLocal<string> LocalTest = CompositionLocalOf(static () => "Default");

        private void Awake()
        {
            if (!Application.isPlaying)
                return;
            new ComposeView().SetContent(MockLayout);
        }

        // [Button("Log")]
        private static void LogButton()
        {
            Debug.Log(CurrentComposer);
        }

        // [Button("Switch")]
        private static void SwitchButton()
        {
            SwitchState.Value = !SwitchState.Value;
        }

        // [Button("Update")]
        private static void UpdateButton()
        {
            UpdateState.Value = !UpdateState.Value;
        }

        // [PropertySpace]
        // [Button("Add")]
        private static void AddButton()
        {
            AddState.Value++;
        }

        // [Button("Remove")]
        private static void RemoveButton()
        {
            AddState.Value = Math.Clamp(AddState.Value - 1, 0, 1000);
        }

        [Composable]
        private static void MockLayout()
        {
            // Debug.Log("MockLayout()");
            _ = UpdateState.Value;
            if (SwitchState.Value)
            {
                // MockColumn(() =>
                // {
                MockSpacer();
                MockSpacer();
                MockSpacer();
                MockSpacer();
                MockSpacer();
                // });
            }

            // MockColumn(() =>
            // {
            MockSpacer();
            // MockSpacer();
            // });

            // MockColumn(() =>
            // {
            for (var i = 0; i < AddState.Value; i++)
                MockSpacer();
            // });
        }

        [Composable]
        private static void MockColumn(ComposableContent content)
        {
            content();
        }

        [Composable]
        private static void MockSpacer()
        {
            // Debug.Log("MockSpacer()");
            MockNestedSpacer();
        }

        [Composable]
        private static void MockNestedSpacer()
        {
            // MockSuperNestedSpacer();
        }

        private static void MockSuperNestedSpacer()
        {
        }

        [Composable]
        private static void MockPerformanceLayout()
        {
            var state = new object();
            var time = TimeUtils.Measure(() =>
            {
                for (var i = 0; i < 1_000_000; i++)
                {
                    _ = Remember(() => new object());
                }
            });
            Debug.Log((int)time.TotalMilliseconds);
        }
    }
}