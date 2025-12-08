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
        __composer.StartRestartGroup(-1780379594);
        if (__composer.ShouldExecute(__content))
        {
            CurrentComposer.StartReusableGroup(0);
            CurrentComposer.SetVisualElement(this);
            CurrentComposer.EnterVisualElement();
            CompositionLocalProvider(LocalVisualElement.Provides(this), LocalLayoutMeasurer.Provides(new LayoutMeasurerImpl(this)), content: content);
            CurrentComposer.EndReusableGroup(0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1780379594)?.UpdateScope(() => __ContentImpl(__content));
    }
}