#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System.Collections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class FocusableSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(823488914);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var focusManager = LocalFocusManager.Current;
                StartCoroutine(InputCoroutine());
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Row(modifier: Modifier.Background(Color.white).Clip(RoundedCornerShape(16.Dp())).Padding(all: 16.Dp()), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        Repeat(3, (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<int>>() : __composer.UpdateRememberedValue<global::System.Action<int>>(it =>
                        {
                            __Column(() =>
                            {
                                __FocusableItem($"{it} first", isDefault: it == 0, __composer: __composer, __changed: 0b_00_00);
                                __FocusableItem($"{it} second", __composer: __composer, __changed: 0b_01_00);
                                __FocusableItem($"{it} third", __composer: __composer, __changed: 0b_01_00);
                                __FocusableItem($"{it} fourth", __composer: __composer, __changed: 0b_01_00);
                            }, __composer: __composer, __changed: 0b_01_01_01_00);
                        })));
                    })), __composer: __composer, __changed: 0b_01_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
                __composer.EndRestartGroup(823488914, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
                return;
                IEnumerator InputCoroutine()
                {
                    while (true)
                    {
                        // if (Input.GetKeyDown(KeyCode.UpArrow))
                        //     focusManager.MoveFocus(FocusDirection.Up);
                        // else if (Input.GetKeyDown(KeyCode.DownArrow))
                        //     focusManager.MoveFocus(FocusDirection.Down);
                        // else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        //     focusManager.MoveFocus(FocusDirection.Left);
                        // else if (Input.GetKeyDown(KeyCode.RightArrow))
                        //     focusManager.MoveFocus(FocusDirection.Right);
                        yield return null;
                    }
                }
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(823488914, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(157955176);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Content(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(157955176, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __FocusableItem(string name, bool isDefault = false, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__name, __isDefault) = (name, isDefault);
            var __isCreated = __composer.StartRestartGroup(1353104298);
            var __dirty = __changed;
            if ((__changed & 0b_00_11) == 0)
                __dirty |= __composer.Changed(name) ? 0b_00_10 : 0b_00_01;
            if ((__changed & 0b_11_00) == 0)
                __dirty |= __composer.Changed(isDefault) ? 0b_10_00 : 0b_01_00;
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01)
            {
                var isFocused = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                var focusRequester = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.FocusRequester>() : __composer.UpdateRememberedValue<global::UnityCompose.FocusRequester>(new FocusRequester()));
                __composer.StartReplaceGroup(1407512844);
                if (isDefault)
                    __SideEffect(0, focusRequester.RequestFocus, __composer: __composer, __changed: 0b_00_01);
                __composer.EndReplaceGroup(1407512844);
                __Spacer(Modifier.Margin(all: 4.Dp()).Size(width: 100.Dp(), height: 40.Dp()).Clip(RoundedCornerShape(16.Dp())).Background(__AnimateColorAsState(isFocused.Value ? Color.lightSeaGreen : Color.indianRed, __composer: __composer, __changed: 0b_01_00).Value).Scale(__AnimateFloatAsState(isFocused.Value ? 1.1f : 1f, __composer: __composer, __changed: 0b_01_00).Value).Name(name).Focusable().OnFocusChanged((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isFocused!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.FocusState>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.FocusState>>(it => isFocused.Value = it.IsFocused))).FocusRequester(focusRequester), __composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01;
            __composer.EndRestartGroup(1353104298, __isRestarted)?.UpdateScope(() => __FocusableItem(__name, __isDefault, __composer, __composer.UpdateChangedFlags(__changed)));
        }
    }
}