using System;
using Compose.Net;
using UnityCompose;
using UnityCompose.Packages.UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

[UxmlElement]
public partial class ComposeView : VisualElement
{
    private Action _content;
    private readonly IReusableComposeNode _rootNode;
    private readonly IComposer _composer = IComposer.Create();

    public ComposeView()
    {
        pickingMode = PickingMode.Ignore;
        _rootNode = new UnityReusableComposeNode(this, true);
    }

    public void SetContent([Composable] Action content)
    {
        if (_content == content)
            return;
        _content = content;
        Bootstrap(
            adapter: UnityComposeAdapter.Instance,
            composer: _composer,
            preview: false,
            rootNode: _rootNode,
            content: content
        );
    }
}