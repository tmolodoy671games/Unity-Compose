// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

[ExecuteAlways]
[DefaultExecutionOrder(-1)]
public abstract partial class ComposeUI : MonoBehaviour
{
    [Composable]
    protected abstract void Content();

    [Composable]
    protected virtual void Preview()
    {
    }

    private ComposeView? _composeView;

    [SuppressMessage("Compose.Net", "CN_COMPOSABLE_PARAMETER:Non composable argument passed to composable parameter")]
    private void OnEnable()
    {
        var document = GetComponent<UIDocument>();
        if (!document) return;
        _composeView = document.rootVisualElement?.Q<ComposeView>();
        _composeView?.SetContent(ApplicationUtils.IsPlaying ? Content : Preview);
    }
    
    private void OnDestroy()
    {
        _composeView?.Dispose();
        _composeView = null;
    }
}