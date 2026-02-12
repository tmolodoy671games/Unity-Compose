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
        protected void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(399654635);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(399654635, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        private void __Content()
        {
            __Content(CurrentComposer, 0b_10);
        }

        protected void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1527080882);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1527080882, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private void __Preview()
        {
            __Preview(CurrentComposer, 0b_10);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1213414619);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var showMovingSquare = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(true)));
                    __composer.StartReplaceGroup(871911557);
                    if (showMovingSquare.Value)
                    {
                        var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                        var offset = __AnimateFloatAsState(targetValue: isSwitched.Value ? 100 : -100, animationSpec: Tween(duration: 3), __composer: __composer, __changed: 0).Value;
                        __Box((!__composer.ChangedAsStruct((isSwitched, offset)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => __Box((!__composer.ChangedAsStruct((isSwitched, offset)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => __Spacer(Modifier.Size(100.Px()).Background(Color.green).Offset(offset.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value))), __composer: __composer, __changed: 0))), __composer: __composer, __changed: 0b_01_01_00))), __composer: __composer, __changed: 0b_01_01_00);
                    }

                    __composer.EndReplaceGroup(871911557);
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(horizontal: 32.Px() + 32 * __AnimateFloatAsState(isHovered.Value.ToInt(), __composer: __composer, __changed: 0b_01_00).Value.Px(), vertical: 16.Px()).Border(16.Px()).Margin(top: 32.Px()).OnClick((!__composer.Changed(showMovingSquare) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => showMovingSquare.Value = !showMovingSquare.Value))).OnMouseEnter((!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1213414619, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}