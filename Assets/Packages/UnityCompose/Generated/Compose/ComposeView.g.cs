using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using StableCollections;
using UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

public partial class ComposeView
{
    [Composable, DontGenerateComposeGroups]
    private void __ContentImpl(ComposableContent content)
    {
        if (CurrentComposer.BeginRootComposeGroup(this))
            return;
        CompositionLocalProvider(LocalVisualElement.Provides(this), LocalLayoutMeasurer.Provides(new LayoutMeasurerImpl(this)), content: content);
        CurrentComposer.EndRootComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<ComposeView, UnityCompose.ComposableContent>, System.Action>(-41200726, (this, content)) ? CurrentComposer.RememberedValue<ValueTuple<ComposeView, UnityCompose.ComposableContent>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<ComposeView, UnityCompose.ComposableContent>, System.Action>(() => ContentImpl(content)));
    }
}