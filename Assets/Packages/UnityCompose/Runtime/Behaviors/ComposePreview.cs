// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

[ExecuteAlways]
public abstract partial class ComposePreview : MonoBehaviour
{
    [Composable]
    protected abstract void Preview();
    
    private ComposeView? _composeView;

    private void OnEnable()
    {
        if (ApplicationUtils.IsPlaying) return;
        var document = GetComponent<UIDocument>();
        if (!document) return;
        _composeView = document.rootVisualElement?.Q<ComposeView>();
        _composeView?.SetContent(Preview);
    }

    private void OnDisable()
    {
        _composeView?.Dispose();
        _composeView = null;
    }
}