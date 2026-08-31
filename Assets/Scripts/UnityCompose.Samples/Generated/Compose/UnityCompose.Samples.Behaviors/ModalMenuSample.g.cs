#nullable enable
// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;
using StableCollections;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ModalMenuSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(53820963);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var showModalMenu = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                __composer.StartReplaceGroup(1645534520);
                if (showModalMenu.Value)
                {
                    __ModalMenu((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModalMenu!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().Background(Color.black.With(a: 0.9f)), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModalMenu!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => __Spacer(modifier: Modifier.Background(Color.lightYellow).Size(100.Dp()).Clip(RoundedCornerShape(16.Dp())).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModalMenu!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => showModalMenu.Value = false))), __composer: __composer, __changed: 0b_00))), __composer: __composer, __changed: 0b_00_00_00);
                    })), __composer: __composer, __changed: 0b_01_00);
                }

                __composer.EndReplaceGroup(1645534520);
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModalMenu!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.Padding(horizontal: 20.Dp(), vertical: 8.Dp()).Background(Color.lightGreen).Blur(__AnimateFloatAsState(LocalModalMenuTags.Current.IsNotEmpty().ToInt(), __composer: __composer, __changed: 0b_01_00).Value * 10).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModalMenu!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => showModalMenu.Value = true))).Clip(RoundedCornerShape(16.Dp())), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Text(text: "Show modal", color: Color.white, fontSize: 32.Sp(), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00_01);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(53820963, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1881078351);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Content(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1881078351, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }
    }
}