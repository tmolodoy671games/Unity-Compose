using System;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal class ManualCompositionDemo : MonoBehaviour
    {
        [ShowInInspector] private int key;

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
            CurrentComposer.BeginComposeGroup(true, "", key);
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
            ComposeFunctions.Remember(() => 1, "", key);
            Log();
        }

        [Button("HasRememberedValue")]
        public void HasRememberedValueButton()
        {
            CurrentComposer.HasRememberedValue<bool, int>(true, "", key);
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
                BeginComposeGroup(true, 1);
                EndComposeGroup();
                
                BeginComposeGroup(true, 2);
                EndComposeGroup();
                
                BeginComposeGroup(true, 3);
                GetOrCreateVisualElement();
                {
                    BeginComposeGroup(true, 1);
                    GetOrCreateVisualElement();
                    EndComposeGroup();
                
                    BeginComposeGroup(true, 2);
                    EndComposeGroup();
                    
                    BeginComposeGroup(true, 3);
                    GetOrCreateVisualElement();
                    EndComposeGroup();
                }
                EndComposeGroup();
                
                BeginComposeGroup(true, 4);
                GetOrCreateVisualElement();
                EndComposeGroup();
                
                BeginComposeGroup(true, 5);
                GetOrCreateVisualElement();
                EndComposeGroup();
            }
            EndRootComposeGroup();
            Log();
        }

        [Button("Layout With Inserted Group")]
        private static void LayoutWithInsertedGroupButton()
        {
            BeginRootComposeGroup();
            {
                BeginComposeGroup(true, 1);
                EndComposeGroup();
                
                BeginComposeGroup(true, 2);
                EndComposeGroup();
                
                BeginComposeGroup(true, 3);
                EndComposeGroup();
                
                BeginComposeGroup(true, 4);
                EndComposeGroup();
                
                BeginComposeGroup(true, 5);
                EndComposeGroup();
            }
            EndRootComposeGroup();
            Log();
        }
        
        [Button("SkipTo7")]
        private static void SkipTo7Button()
        {
            BeginRootComposeGroup();
            {
                BeginComposeGroup(true, 1);
                EndComposeGroup();

                BeginComposeGroup(true, 2);
                EndComposeGroup();

                BeginComposeGroup(true, 2);
                EndComposeGroup();

                BeginComposeGroup(true, 3);
                {
                    BeginComposeGroup(true, 4);
                    EndComposeGroup();

                    BeginComposeGroup(true, 5);
                    EndComposeGroup();

                    BeginComposeGroup(true, 6);
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
                BeginComposeGroup(true, 1);
                EndComposeGroup();

                BeginComposeGroup(true, 3);
                {
                    BeginComposeGroup(true, 4);
                    EndComposeGroup();

                    BeginComposeGroup(true, 5);
                    EndComposeGroup();

                    // BeginComposeGroup(true, 6);
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
                Remember(1, () => 1, 1);
                Remember(1, () => 1, 1);
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
                BeginComposeGroup(true, 1);
                EndComposeGroup();

                BeginComposeGroup(true, 2);
                EndComposeGroup();

                BeginComposeGroup(true, 3);
                {
                    BeginComposeGroup(true, 4);
                    EndComposeGroup();

                    BeginComposeGroup(true, 5);
                    EndComposeGroup();
                }
                EndComposeGroup();
            }
            EndRootComposeGroup();

            BeginRootComposeGroup();
            {
                BeginComposeGroup(true, 1);
                EndComposeGroup();

                BeginComposeGroup(true, 3);
                {
                    BeginComposeGroup(true, 4);
                    EndComposeGroup();

                    BeginComposeGroup(true, 5);
                    EndComposeGroup();
                }
                EndComposeGroup();
            }
            EndRootComposeGroup();
            Log();
        }

        private static void BeginRootComposeGroup()
        {
            CurrentComposer.BeginRootComposeGroup(new ComposeView());
        }

        private static void EndRootComposeGroup()
        {
            CurrentComposer.EndRootComposeGroup(() => { });
        }

        private static void BeginComposeGroup<T>(T state, int lineNumber)
        {
            CurrentComposer.BeginComposeGroup(state, "", lineNumber);
        }

        private static void EndComposeGroup()
        {
            CurrentComposer.EndComposeGroup(() => { });
        }

        private static TValue Remember<TKey, TValue>(TKey key, Func<TValue> defaultValueFactory, int lineNumber)
        {
            return ComposeFunctions.Remember(key, defaultValueFactory, "", lineNumber);
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