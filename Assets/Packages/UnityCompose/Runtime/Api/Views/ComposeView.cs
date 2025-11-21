using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using StableCollections;
using UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine;
using UnityEngine.UIElements;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
    {
    }

    private Action? _content;

    public void SetContent([Composable] Action content)
    {
        if (_content == content)
            return;
        _content = content;
        userData = null;
        Clear();
        CurrentComposer.Reset();
        ContentImpl(content);
    }

    [Composable, DontGenerateComposeGroups]
    private void ContentImpl(Action content)
    {
        if (CurrentComposer.BeginRootComposeGroup(this)) return;
        CompositionLocalProvider(
            LocalVisualElement.Provides(this),
            LocalLayoutMeasurer.Provides(new LayoutMeasurerImpl(this)),
            content: content
        );
        CurrentComposer.EndRootComposeGroup(() => ContentImpl(content));
    }

    public override string ToString() => "ComposeView";
}