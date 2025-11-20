using System;
using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

public partial class ComposeView
{
    [Composable, DontGenerateComposeGroups]
    private void __ContentImpl(Action content)
    {
        if (CurrentComposer.BeginRootComposeGroup(this))
            return;
        CompositionLocalProvider(LocalVisualElement.Provides(this), LocalLayoutMeasurer.Provides(new LayoutMeasurerImpl(this)), content: content);
        CurrentComposer.EndRootComposeGroup(CurrentComposer.WithState((this, content)).Remember<System.Action>(__ => () => ContentImpl(content)));
    }
}