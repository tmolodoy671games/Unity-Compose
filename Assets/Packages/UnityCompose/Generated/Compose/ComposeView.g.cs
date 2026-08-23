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
        var __isCreated = __composer.StartRestartGroup(755448642);
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10 : 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var composer = __composer;
            composer.StartReusableGroup<ComposeView>(0);
            composer.SetVisualElement(this);
            composer.EnterVisualElement(this);
            __composer.StartReplaceGroup(879732289);
            __CompositionLocalProvider(LocalVisualElement.Provides(this), (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_11) == 0b_10).Changed<global::UnityCompose.Composer>(composer!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>( // LocalIsActive.Provides(isActiveInstance),
            // LocalOnScreenMenuManager.Provides(onScreenManager),
            () => content(composer, 0))), __composer: __composer, __changed: 0b_00_00);
            __composer.EndReplaceGroup(879732289);
            composer.EndReusableGroup(0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(755448642, __isRestarted)?.UpdateScope(() => __ContentImpl(__content, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}