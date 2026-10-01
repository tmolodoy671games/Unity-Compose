using System;
using Compose.Net;
using UnityCompose;
using UnityCompose.Packages.UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

[UxmlElement]
public partial class ComposeView : VisualElement, IDisposable
{
    private ComposableContent? _content;
    private readonly IReusableComposeNode _rootNode;
    private readonly IComposer _composer = IComposer.Create();

    public ComposeView()
    {
        pickingMode = PickingMode.Ignore;
        _rootNode = new UnityReusableComposeNode(this, true);
    }

    public void SetContent(ComposableContent content)
    {
        if (_content == content)
            return;
        _content = content;
        Clear();
        Bootstrap(
            adapter: UnityComposeAdapter.Instance,
            composer: _composer,
            preview: !ApplicationUtils.IsPlaying,
            rootNode: _rootNode,
            config: new ComposeConfig(
                Logging: false
            ),
            content: content
        );
    }

    public void Dispose()
    {
        _composer.Dispose();
        Clear();
    }
}