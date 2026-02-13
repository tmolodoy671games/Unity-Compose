#nullable enable
using System.Diagnostics.CodeAnalysis;
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
        var __isCreated = __composer.StartRestartGroup(24171452);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_11) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_10 : 0b_01;
        }
        else
        {
            __dirtyRestart |= 0b_01;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            var composer = __composer;
            composer.StartReusableGroup(0);
            composer.SetVisualElement(this);
            composer.EnterVisualElement(this);
            __CompositionLocalProvider(LocalVisualElement.Provides(this), (!__composer.ChangedAsStruct((this, content)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => content(_composer, 0))), __composer: __composer, __changed: 0);
            composer.EndReusableGroup(0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(24171452, __isRestarted)?.UpdateScope(() => __ContentImpl(__content, __composer, __dirtyRestart));
    }
}