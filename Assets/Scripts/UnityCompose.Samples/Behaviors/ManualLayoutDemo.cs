using System;
using SharpExtensions;
using Sirenix.OdinInspector;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal partial class ManualLayoutDemo : MonoBehaviour
    {
        private void Awake()
        {
            if (!Application.isPlaying)
                return;
            new ComposeView().SetContent(() =>
            {
                var time = TimeUtils.Measure(MockLayout);
                Debug.Log(time.TotalMilliseconds.ToFloat().ToInt());
            });
        }

        [Button]
        private static void MockLayoutButton()
        {
            CurrentComposer.Clear();
            MockLayout();
            Log();
        }

        [Composable]
        private static void MockLayout()
        {
            for (var i = 0; i < 1_000_000; i++)
            {
                CurrentComposer.StartRestartGroup(i);
                CurrentComposer.EndRestartGroup(i);
                // Spacer(Modifier);
            }
        }

        private static void Log()
        {
            Debug.Log(CurrentComposer);
        }
    }
}