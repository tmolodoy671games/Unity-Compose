#nullable enable
using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose;
using UnityEngine.UIElements;
using System;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

public partial class ComposeView
{
    private void __ContentImpl(ComposableContent<Composer, int> content, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __content = (content);
        var __isCreated = __composer.StartRestartGroup(877442121);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10 : 0b_01;
        else
            __dirtyRestart |= 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var onScreenManager = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ModalMenuManager>() : __composer.UpdateRememberedValue<global::UnityCompose.ModalMenuManager>(new ModalMenuManager()));
            var composer = __composer;
            composer.StartReusableGroup(0);
            composer.SetVisualElement(this);
            composer.EnterVisualElement(this);
            var isActiveInstance = (!__composer.Changed<bool>(onScreenManager.Contents.IsEmpty()!) ? __composer.RememberedValue<global::UnityCompose.IsActiveEntry>() : __composer.UpdateRememberedValue<global::UnityCompose.IsActiveEntry>(new IsActiveEntry(onScreenManager.Contents.IsEmpty(), null)));
            __composer.StartReplaceGroup(670603868);
            __CompositionLocalProvider(LocalVisualElement.Provides(this), LocalIsActive.Provides(isActiveInstance), LocalOnScreenMenuManager.Provides(onScreenManager), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11) == 0b_10).Changed<global::UnityCompose.Composer>(composer!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => content(composer, 0))), __composer: __composer, __changed: 0b_00_00_00_00);
            __composer.EndReplaceGroup(670603868);
            __composer.StartReplaceGroup(1348373615);
            foreach (var overlayContent in onScreenManager.Contents)
            {
                __Box(modifier: Modifier.OnClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                {
                }))).FillMaxSize().Float(), content: overlayContent, __composer: __composer, __changed: 0b_01_00_00);
            }

            __composer.EndReplaceGroup(1348373615);
            composer.EndReusableGroup(0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(877442121, __isRestarted)?.UpdateScope(() => __ContentImpl(__content, __composer, __dirtyRestart));
    }
}