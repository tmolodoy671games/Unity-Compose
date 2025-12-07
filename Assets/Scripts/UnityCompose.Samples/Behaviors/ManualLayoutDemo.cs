using System;
using SharpExtensions;
using Sirenix.OdinInspector;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal partial class ManualLayoutDemo : MonoBehaviour
    {
        private static readonly IMutableState<bool> UpdateState = MutableStateOf(false);
        private static readonly IMutableState<bool> SwitchState = MutableStateOf(false);

        private void Awake()
        {
            if (!Application.isPlaying)
                return;
            new ComposeView().SetContent(MockLayout);
        }

        [Button("Log")]
        private static void LogButton()
        {
            Debug.Log(CurrentComposer);
        }

        [Button("Switch")]
        private static void SwitchButton()
        {
            SwitchState.Value = !SwitchState.Value;
        }

        [Button("Update")]
        private static void UpdateButton()
        {
            UpdateState.Value = !UpdateState.Value;
        }

        [Composable]
        private static void MockLayout()
        {
            Debug.Log("MockLayout()");
            var _ = UpdateState.Value;
            MockSpacer();
            if (SwitchState.Value)
            {
                MockSpacer();
            }
        }

        [Composable]
        private static void MockSpacer()
        {
            Debug.Log("MockSpacer()");
        }

        [Composable]
        private static void MockPerformanceLayout()
        {
            for (var i = 0; i < 1_000_000; i++)
            {
                EmptyComposable();
            }
        }

        [Composable]
        private static void EmptyComposable()
        {
        }

        private static void Log()
        {
            Debug.Log(CurrentComposer);
        }
    }
}