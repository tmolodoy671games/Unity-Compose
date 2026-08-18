#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ContentSwapSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1629007819);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1629007819, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(86711710);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(86711710, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1338862325);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    if (isSwitched.Value)
                    {
                        __composer.StartReplaceGroup(955378125);
                        __Content2(__composer: __composer, __changed: 0b_00);
                        __composer.EndReplaceGroup(955378125);
                    }
                    else
                    {
                        __composer.StartReplaceGroup(1284654179);
                        __Content1(__composer: __composer, __changed: 0b_00);
                        __composer.EndReplaceGroup(1284654179);
                    }

                    __Text(text: "Switch", color: Color.white, fontSize: 62, modifier: Modifier.Padding(horizontal: 20.Px(), vertical: 12.Px()).Border(16.Px()).Background(Color.blue).Margin(top: 16.Px()).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                })), __composer: __composer, __changed: 0b_00_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1338862325, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Content1(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1026411864);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Spacer(Modifier.Size(100.Px()).Background(Color.green), __composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1026411864, __isRestarted)?.UpdateScope(() => __Content1(__composer, 0));
        }

        private static void __Content2(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(23616230);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Spacer(Modifier.Size(100.Px()).Background(Color.red), __composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(23616230, __isRestarted)?.UpdateScope(() => __Content2(__composer, 0));
        }
    }
}