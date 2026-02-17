#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1544580258);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1544580258, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(104297156);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(104297156, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1890842087);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isRedSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Spacer(Modifier.Size(100.Px()).Background(Color.red).Border(16.Px()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isRedSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isRedSwitched.Value = !isRedSwitched.Value))).Scale(__AnimateFloatAsState(isRedSwitched.Value ? 1.5f : 1f, __composer: __composer, __changed: 0b_01_00).Value).Margin(top: 40.Px()), __composer: __composer, __changed: 0b_00);
                    var isGreenSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Spacer(Modifier.Size(100.Px()).Background(Color.green).Border(16.Px()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isGreenSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isGreenSwitched.Value = !isGreenSwitched.Value))).Scale(__AnimateFloatAsState(isGreenSwitched.Value ? 1.5f : 1f, __composer: __composer, __changed: 0b_01_00).Value).Margin(top: 40.Px()), __composer: __composer, __changed: 0b_00);
                    var isBlueSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Spacer(Modifier.Size(100.Px()).Background(Color.blue).Border(16.Px()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isBlueSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isBlueSwitched.Value = !isBlueSwitched.Value))).Scale(__AnimateFloatAsState(isBlueSwitched.Value ? 1.5f : 1f, __composer: __composer, __changed: 0b_01_00).Value).Margin(top: 40.Px()), __composer: __composer, __changed: 0b_00);
                })), __composer: __composer, __changed: 0b_00_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1890842087, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}