#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors.UpdatePerformanceTest
{
    internal partial class ComposeUpdatePerformanceTest
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(986482196);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = -1;
                var parentSize = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(MutableStateOf(Vector2.zero)));
                __Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned((!__composer.Changed<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(parentSize!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => parentSize.Value = it.Size))), content: (!__composer.Changed<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(parentSize!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(1694415790);
                    for (var i = 0; i < 1_000; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(parentSize!).Changed<int>(currentI!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Item(currentI, parentSize.Value, __composer: __composer, __changed: 0b_00_00))));
                    }

                    __composer.EndReplaceGroup(1694415790);
                })), __composer: __composer, __changed: 0b_01_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(986482196, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        private static void __Item(int currentI, Vector2 parentSize, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__currentI, __parentSize) = (currentI, parentSize);
            var __isCreated = __composer.StartRestartGroup(1452761585);
            var __dirty = __changed;
            if ((__changed & 0b_00_11) == 0)
                __dirty |= __composer.Changed(currentI) ? 0b_00_10 : 0b_00_01;
            if ((__changed & 0b_11_00) == 0)
                __dirty |= __composer.Changed(parentSize) ? 0b_10_00 : 0b_01_00;
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01)
            {
                var position = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(MutableStateOf(Vector2.zero)));
                __LaunchedEffect(key: parentSize, coroutine: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11_00) == 0b_10_00).Changed<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(position!).Get() ? __composer.RememberedValue<global::System.Func<global::System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<global::System.Func<global::System.Collections.IEnumerator>>(() => PerformanceUtils.MoveRandomlyCoroutine(parentSize: () => parentSize, it => position.Value = it))), __composer: __composer, __changed: ((__dirty & 0b_11_00) >> 2));
                var baseModifier = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11) == 0b_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.IModifier>() : __composer.UpdateRememberedValue<global::UnityCompose.IModifier>(Modifier.Size(50.Dp()).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float()));
                __Spacer(modifier: baseModifier.Position(left: position.Value.x.Dp(), top: position.Value.y.Dp()), __composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01;
            __composer.EndRestartGroup(1452761585, __isRestarted)?.UpdateScope(() => __Item(__currentI, __parentSize, __composer, __composer.UpdateChangedFlags(__changed)));
        }
    }
}