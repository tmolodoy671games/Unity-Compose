using System;
using System.Collections;
using SharpExtensions;
using Sirenix.OdinInspector;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal class ManualCompositionDemo : MonoBehaviour
    {
        [ShowInInspector] private int key;
        
        [PropertySpace]
        [Button("Start Reusable Group")]
        private void StartReusableGroupButton()
        {
            CurrentComposer.StartReusableGroup(key);
            Log();
        }

        [Button("End Reusable Group")]
        private void EndReusableGroupButton()
        {
            CurrentComposer.EndReusableGroup(key);
            Log();
        }

        [PropertySpace]
        [Button("Start Restart Group")]
        private void StartRestartGroupButton()
        {
            CurrentComposer.StartRestartGroup(key);
            Log();
        }

        [Button("End Restart Group")]
        private void EndRestartGroupButton()
        {
            CurrentComposer.EndRestartGroup(key, false);
            Log();
        }

        [PropertySpace]
        [Button("Start Replace Group")]
        private void StartReplaceGroupButton()
        {
            CurrentComposer.StartReplaceGroup(key);
            Log();
        }

        [Button("End Replace Group")]
        private void EndReplaceGroupButton()
        {
            CurrentComposer.EndReplaceGroup(key);
            Log();
        }

        [PropertySpace]
        [Button("Log")]
        private void LogButton()
        {
            Log();
        }

        [PropertySpace]
        [Button("Clear")]
        private void ClearButton()
        {
            CurrentComposer.Clear();
        }

        [PropertySpace]
        [Button("ResetTo")]
        private void ResetToButton()
        {
            CurrentComposer.Clear();
        }

        private static void Log()
        {
            Debug.Log(CurrentComposer);
        }
    }
}