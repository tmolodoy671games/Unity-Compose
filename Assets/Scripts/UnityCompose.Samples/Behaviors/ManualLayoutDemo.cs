using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using StableCollections;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
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
            new ComposeView().SetContent(MockPerformanceLayout);
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

        [PropertySpace]
        [Button("Add")]
        private static void AddButton()
        {
            AddState.Value++;
        }

        [Button("Remove")]
        private static void RemoveButton()
        {
            AddState.Value = Math.Clamp(AddState.Value - 1, 0, 1000);
        }
        
        [Composable]
        private static void MockLayout()
        {
            // Debug.Log(LocalTest.Current);
            // CompositionLocalProvider(
            //     LocalTest.Provides("Custom1"),
            //     () =>
            //     {
            //         Debug.Log(LocalTest.Current);
            //         CompositionLocalProvider(
            //             LocalTest.Provides("Custom2"),
            //             () => Debug.Log(LocalTest.Current)
            //         );
            //     }
            // );
            // Debug.Log(LocalTest.Current);

            Debug.Log("MockLayout()");
            var _ = UpdateState.Value;
            // MockSpacer();
            // if (SwitchState.Value)
            // {
            //     MockSpacer();
            // }
            //
            // for (var i = 0; i < AddState.Value; i++)
            // {
            //     MockSpacer();
            // }
        }

        [Composable]
        private static void MockSpacer()
        {
            Debug.Log("MockSpacer()");
        }

        [Composable]
        private static void MockPerformanceLayout()
        {
            var composer = CurrentComposer;
            var list = Remember(() => IImmutableStableList.Create<CompositionLocalProvides>());
            var state = new object();
            var time = TimeUtils.Measure(() =>
            {
                for (var i = 0; i < 1_000_000; i++)
                {
                    var _ = Remember(() => state);
                }
            });
            Debug.Log((int)time.TotalMilliseconds);
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