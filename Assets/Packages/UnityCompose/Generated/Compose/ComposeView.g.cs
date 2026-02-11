#nullable enable
using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityEngine.UIElements;
using System;
using static UnityCompose.ComposeFunctions;

public partial class ComposeView
{
    private void __ContentImpl(ComposableContent content, global::UnityCompose.Composer __composer = null !)
    {
        var __content = (content);
        __composer.StartRestartGroup(-24171452);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__content))
        {
            var composer = __composer;
            composer.StartReusableGroup(0);
            composer.SetVisualElement(this);
            composer.EnterVisualElement(this);
            CompositionLocalProvider(LocalVisualElement.Provides(this), content);
            composer.EndReusableGroup(0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-24171452, __isRestarted)?.UpdateScope(() => __ContentImpl(__content));
    }

    private void __ContentImpl(ComposableContent content)
    {
        __ContentImpl(content, CurrentComposer);
    }
}