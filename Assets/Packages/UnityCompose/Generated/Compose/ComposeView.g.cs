using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityEngine.UIElements;
using System;
using static UnityCompose.ComposeFunctions;

public partial class ComposeView
{
    [Composable]
    private void __ContentImpl(ComposableContent content)
    {
        var __content = (content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-418571067);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__content))
        {
            var composer = CurrentComposer;
            composer.StartReusableGroup(0);
            composer.SetVisualElement(this);
            composer.EnterVisualElement(this);
            content();
            composer.EndReusableGroup(0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-418571067, __isRestarted)?.UpdateScope(() => __ContentImpl(__content));
    }
}