#nullable enable
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;
internal partial class SystemScreen
{
    public override void __Content(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(1085613212);
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var showModals = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
            __composer.StartReplaceGroup(119848636);
            if (showModals.Value)
            {
                __ModalMenu((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModals!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Spacer(Modifier.Size(100.Px()).Background(Color.red).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModals!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => showModals.Value = false))), __composer: __composer, __changed: 0b_00);
                })), __composer: __composer, __changed: 0b_00);
            }

            __composer.EndReplaceGroup(119848636);
            __Box(alignment: Alignment.Center, modifier: modifier.FillMaxSize(), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModals!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                __Column((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModals!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __DsClickIndication((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModals!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent<global::UI.DesignSystem.Compose.DsClickIndicationScope>>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent<global::UI.DesignSystem.Compose.DsClickIndicationScope>>(it =>
                    {
                        __Text("Bla", modifier: Modifier.OnClick((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(showModals!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => showModals.Value = true))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01);
                    })), __composer: __composer, __changed: 0b_01_01_01_01_00);
                    __DsClickIndication((!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent<global::UI.DesignSystem.Compose.DsClickIndicationScope>>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent<global::UI.DesignSystem.Compose.DsClickIndicationScope>>(it =>
                    {
                        __Text("Bla", modifier: Modifier, __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01);
                    })), __composer: __composer, __changed: 0b_01_01_01_01_00);
                })), __composer: __composer, __changed: 0b_01_01_01_00);
            })), __composer: __composer, __changed: 0b_00_00_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(1085613212, __isRestarted)?.UpdateScope(() => __Content(__modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}