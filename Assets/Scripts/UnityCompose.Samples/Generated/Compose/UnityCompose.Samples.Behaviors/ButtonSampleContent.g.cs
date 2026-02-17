#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1715061779);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1715061779, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(817644427);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(817644427, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(892060343);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Foo(isHovered.Value, __composer: __composer, __changed: 0b_00);
                    __Box(modifier: Modifier.Padding(horizontal: __AnimateFloatAsState(isHovered.Value ? 80 : 40, __composer: __composer, __changed: 0b_01_00).Value.Px(), vertical: 16.Px()).Background(Color.blue).Border(radius: 16.Px()).OnMouseEnter((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false))), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __Text(text: "Click me", fontSize: 24, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_01);
                        })), __composer: __composer, __changed: 0b_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(892060343, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Foo(bool param, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __param = (param);
            var __isCreated = __composer.StartRestartGroup(874187044);
            var __dirty = __changed;
            var __dirtyRestart = 0;
            if ((__changed & 0b_11) == 0)
            {
                __dirty |= __composer.Changed(param) ? 0b_10 : 0b_01;
            }
            else
            {
                __dirtyRestart |= 0b_01;
            }

            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01)
            {
                __Bar(param, __AnimateFloatAsState(param ? 1 : 0, __composer: __composer, __changed: 0b_01_00).Value, __composer: __composer, __changed: (__dirty & 0b_00_11));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01;
            __composer.EndRestartGroup(874187044, __isRestarted)?.UpdateScope(() => __Foo(__param, __composer, __dirtyRestart));
        }

        private static void __Bar(bool param, float param2, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__param, __param2) = (param, param2);
            var __isCreated = __composer.StartRestartGroup(1875170232);
            var __dirty = __changed;
            var __dirtyRestart = 0;
            if ((__changed & 0b_00_11) == 0)
            {
                __dirty |= __composer.Changed(param) ? 0b_00_10 : 0b_00_01;
            }
            else
            {
                __dirtyRestart |= 0b_00_01;
            }

            if ((__changed & 0b_11_00) == 0)
            {
                __dirty |= __composer.Changed(param2) ? 0b_10_00 : 0b_01_00;
            }
            else
            {
                __dirtyRestart |= 0b_01_00;
            }

            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01)
            {
                __LaunchedEffect(param, (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11) == 0b_00_10).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log(param))), __composer: __composer, __changed: (__dirty & 0b_00_11));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01;
            __composer.EndRestartGroup(1875170232, __isRestarted)?.UpdateScope(() => __Bar(__param, __param2, __composer, __dirtyRestart));
        }
    }
}