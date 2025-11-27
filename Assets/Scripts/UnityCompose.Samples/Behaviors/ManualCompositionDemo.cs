using System;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal class ManualCompositionDemo : MonoBehaviour
    {
        [ShowInInspector] private int key;
        
        private static readonly IMutableState<bool> _mutableState = MutableStateOf(false);

        [Button("BeginRootComposeGroup")]
        private static void BeginRootComposeGroupButton()
        {
            CurrentComposer.BeginRootComposeGroup(new ComposeView());
            Log();
        }

        [Button("EndRootComposeGroup")]
        private static void EndRootComposeGroupButton()
        {
            CurrentComposer.EndRootComposeGroup(() => { });
            Log();
        }

        [PropertySpace]
        [Button("BeginComposeGroup")]
        private void BeginComposeGroupButton()
        {
            CurrentComposer.BeginComposeGroup(key, true);
            Log();
        }

        [Button("EndComposeGroup")]
        private static void EndComposeGroupButton()
        {
            CurrentComposer.EndComposeGroup(() => { });
            Log();
        }

        [PropertySpace]
        [Button("Remember")]
        private void RememberButton()
        {
            ComposeFunctions.Remember(() => 1);
            Log();
        }

        [Button("HasRememberedValue")]
        public void HasRememberedValueButton()
        {
            CurrentComposer.HasRememberedValue<bool, int>(key, true);
            Log();
        }

        [Button("RememberedValue")]
        public void RememberedValueButton()
        {
            CurrentComposer.RememberedValue<bool, int>();
            Log();
        }

        [Button("WriteValue")]
        public void WriteValueButton()
        {
            CurrentComposer.WriteValue<bool, int>(() => 1);
            Log();
        }

        [PropertySpace]
        [Button("GetOrCreateVisualElement")]
        private void GetOrCreateVisualElementButton()
        {
            CurrentComposer.GetOrCreateVisualElement<VisualElement>();
            Log();
        }

        [PropertySpace]
        [Button]
        private void Clear()
        {
            CurrentComposer.Reset();
            Log();
        }

        [PropertySpace]
        [Button("Log")]
        private void LogButton()
        {
            Log();
        }

        [PropertySpace]
        [Button("Initial Layout")]
        private static void InitialLayoutButton()
        {
            BeginRootComposeGroup();
            {
                BeginComposeGroup(1, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(3, true);
                {
                    BeginComposeGroup(4, true);
                    EndComposeGroup();

                    BeginComposeGroup(5, true);
                    EndComposeGroup();

                    BeginComposeGroup(6, true);
                    EndComposeGroup();
                }
                EndComposeGroup();

                BeginComposeGroup(7, true);
                EndComposeGroup();

                BeginComposeGroup(8, true);
                EndComposeGroup();

                BeginComposeGroup(9, true);
                EndComposeGroup();
            }
            EndComposeGroup();
            Log();
        }

        [Button("Layout With Inserted Group")]
        private static void LayoutWithInsertedGroupButton()
        {
            BeginRootComposeGroup();
            {
                BeginComposeGroup(1, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(3, true);
                {
                    BeginComposeGroup(4, true);
                    EndComposeGroup();

                    BeginComposeGroup(5, true);
                    EndComposeGroup();

                    BeginComposeGroup(6, true);
                    EndComposeGroup();
                }
                EndComposeGroup();

                BeginComposeGroup(7, true);
                EndComposeGroup();

                BeginComposeGroup(7, true);
                EndComposeGroup();

                BeginComposeGroup(7, true);
                EndComposeGroup();

                BeginComposeGroup(7, true);
                EndComposeGroup();

                BeginComposeGroup(8, true);
                EndComposeGroup();

                BeginComposeGroup(9, true);
                EndComposeGroup();
            }
            EndComposeGroup();
            Log();
        }
        
        [Button("SkipTo7")]
        private static void SkipTo7Button()
        {
            BeginRootComposeGroup();
            {
                BeginComposeGroup(1, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(3, true);
                {
                    BeginComposeGroup(4, true);
                    EndComposeGroup();

                    BeginComposeGroup(5, true);
                    EndComposeGroup();

                    BeginComposeGroup(6, true);
                    EndComposeGroup();
                }
                EndComposeGroup();
            }
            Log();
        }

        [Button("Layout With Removed Group")]
        private static void LayoutWithRemovedGroupButton()
        {
            BeginRootComposeGroup();
            {
                BeginComposeGroup(1, true);
                EndComposeGroup();

                BeginComposeGroup(3, true);
                {
                    BeginComposeGroup(4, true);
                    EndComposeGroup();

                    BeginComposeGroup(5, true);
                    EndComposeGroup();

                    // BeginComposeGroup(6, true);
                    // EndComposeGroup();
                }
                EndComposeGroup();
            }
            EndRootComposeGroup();
            Log();
        }

        [Button("Remember Layout")]
        private static void RememberLayoutButton()
        {
            BeginRootComposeGroup();
            {
                Remember(true, () => 1);
                Remember(true, () => 1);
            }
            EndRootComposeGroup();
            Log();
        }

        [PropertySpace]
        [Button("Test")]
        private static void TestButton()
        {
            // CurrentComposer.Test();
            BeginRootComposeGroup();
            {
                BeginComposeGroup(1, true);
                EndComposeGroup();

                BeginComposeGroup(2, true);
                EndComposeGroup();

                BeginComposeGroup(3, true);
                {
                    BeginComposeGroup(4, true);
                    EndComposeGroup();

                    BeginComposeGroup(5, true);
                    EndComposeGroup();
                }
                EndComposeGroup();
            }
            EndRootComposeGroup();

            BeginRootComposeGroup();
            {
                BeginComposeGroup(1, true);
                EndComposeGroup();

                BeginComposeGroup(3, true);
                {
                    BeginComposeGroup(4, true);
                    EndComposeGroup();

                    BeginComposeGroup(5, true);
                    EndComposeGroup();
                }
                EndComposeGroup();
            }
            EndRootComposeGroup();
            Log();
        }

        [Button("Restart")]
        private static void RestartButton()
        {
            _mutableState.Value = !_mutableState.Value;
        }

        private static void BeginRootComposeGroup()
        {
            CurrentComposer.BeginRootComposeGroup(new ComposeView());
        }

        private static void EndRootComposeGroup()
        {
            CurrentComposer.EndRootComposeGroup(() => { });
        }

        private static void BeginComposeGroup<T>(int groupKey, T state)
        {
            CurrentComposer.BeginComposeGroup(groupKey, state);
        }

        private static void EndComposeGroup()
        {
            CurrentComposer.EndComposeGroup(() => { });
        }

        private static TValue Remember<TKey, TValue>(TKey key, Func<TValue> defaultValueFactory)
        {
            return ComposeFunctions.Remember(key, defaultValueFactory);
        }

        private static VisualElement GetOrCreateVisualElement()
        {
            return CurrentComposer.GetOrCreateVisualElement<VisualElement>();
        }

        private static void Log()
        {
            Debug.Log(CurrentComposer);
        }
    }
}