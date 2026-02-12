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
        protected void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1715061779);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1715061779, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        private void __Content()
        {
            __Content(CurrentComposer, 0b_10);
        }

        protected void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(817644427);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(817644427, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private void __Preview()
        {
            __Preview(CurrentComposer, 0b_10);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(892060343);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    // var count = Remember(() => MutableStateOf(1));
                    //
                    // Text(
                    //     text: count.Value.ToString(),
                    //     color: Color.white,
                    //     modifier: Modifier
                    //         .Background(Color.blue)
                    //         .Padding(horizontal: AnimateFloatAsState(40 + 2 * count.Value).Value.Px(), vertical:20.Px())
                    //         .Border(16.Px())
                    //         .OnClick(() => count.Value++)
                    // );
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    __Box(modifier: Modifier.Padding(horizontal: __AnimateFloatAsState(isHovered.Value ? 80 : 40, __composer: __composer, __changed: 0b_01_00).Value.Px(), vertical: 16.Px()).Background(Color.blue).Border(radius: 16.Px()).OnMouseEnter((!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed(isHovered) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isHovered.Value = false))), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        __CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            __Text(text: "Click me", fontSize: 24, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01_01);
                        })), __composer: __composer, __changed: 0);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(892060343, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer, 0b_10);
        }
    }
}