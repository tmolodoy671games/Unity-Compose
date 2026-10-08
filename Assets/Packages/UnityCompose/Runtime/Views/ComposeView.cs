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
    private bool _isDisposed;

    public ComposeView()
    {
        pickingMode = PickingMode.Ignore;
        _rootNode = new UnityReusableComposeNode(this, true);
    }

    public void SetContent(ComposableContent content)
    {
        if (_content == content && !_isDisposed)
            return;
        Dispose();
        _isDisposed = false;
        _content = content;
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
        if (_isDisposed)
            return;
        _isDisposed = true;
        _composer.Dispose();
        Clear();
    }
}