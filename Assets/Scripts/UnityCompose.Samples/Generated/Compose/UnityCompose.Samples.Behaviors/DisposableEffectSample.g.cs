#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class DisposableEffectSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1063984080);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1063984080, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1131199242);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1131199242, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(2069458287);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isEffectRunning = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __composer.StartReplaceGroup(940161317);
                    if (isEffectRunning.Value)
                    {
                        __DisposableEffect(string.Empty, (!__composer.Changed() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>(it =>
                        {
                            Debug.Log("DisposableEffect()");
                            return it.OnDispose(() => Debug.Log("OnDispose()"));
                        })), __composer: __composer, __changed: 0b_00_00);
                    }

                    __composer.EndReplaceGroup(940161317);
                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Text(text: $"DisposableEffect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20.Px()).Padding(horizontal: isHovered.Value ? 40.Px() : 20.Px(), transition: Transition()).Border(radius: 16.Px()).Margin(top: 32.Px()).OnMouseEnter((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false))).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isEffectRunning!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(2069458287, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}