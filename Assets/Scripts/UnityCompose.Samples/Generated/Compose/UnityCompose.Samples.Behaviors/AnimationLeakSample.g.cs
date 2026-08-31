#nullable enable
// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimationLeakSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(399654635);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(399654635, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1527080882);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1527080882, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1213414619);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var showMovingSquare = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(true)));
                    __composer.StartReplaceGroup(871911557);
                    if (showMovingSquare.Value)
                    {
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        var offset = __AnimateFloatAsState(targetValue: isSwitched.Value ? 100 : -100, animationSpec: Tween(duration: 3), __composer: __composer, __changed: 0b_00_00).Value;
                        __Box((!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!).Changed<float>(offset!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Box((!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!).Changed<float>(offset!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Spacer(Modifier.Size(100.Dp()).Background(Color.green).Offset(offset.Dp()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value))), __composer: __composer, __changed: 0b_00))), __composer: __composer, __changed: 0b_01_01_00))), __composer: __composer, __changed: 0b_01_01_00);
                    }

                    __composer.EndReplaceGroup(871911557);
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Text(text: "Switch", color: Color.white, fontSize: 32.Sp(), modifier: Modifier.Background(Color.blue).Padding(horizontal: 32.Dp() + 32 * __AnimateFloatAsState(isHovered.Value.ToInt(), __composer: __composer, __changed: 0b_01_00).Value.Dp(), vertical: 16.Dp()).Clip(RoundedCornerShape(16.Dp())).Margin(top: 32.Dp()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showMovingSquare!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => showMovingSquare.Value = !showMovingSquare.Value))).OnMouseEnter((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_01);
                })), __composer: __composer, __changed: 0b_00_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1213414619, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}