using System;
using SharpExtensions;
using Sirenix.OdinInspector;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal partial class ManualLayoutDemo : MonoBehaviour
    {
        private static readonly IMutableState<bool> State = MutableStateOf(false);
        
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
            State.Value = !State.Value;
        }

        [Composable]
        private static void MockLayout()
        {
            Debug.Log("MockLayout()");
            var _ = State.Value.ToString();
            // MockSpacer();
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