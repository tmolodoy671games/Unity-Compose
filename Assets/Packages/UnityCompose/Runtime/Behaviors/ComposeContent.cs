// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

[ExecuteAlways]
public abstract partial class ComposeContent : MonoBehaviour
{
    [Composable]
    protected abstract void Content();

    [Composable]
    protected virtual void Preview()
    {
    }

    [SuppressMessage("Compose.Net", "CN_COMPOSABLE_PARAMETER:Non composable argument passed to composable parameter")]
    private void OnEnable()
    {
        var document = GetComponent<UIDocument>();
        if (!document) return;
        var composeView = document.rootVisualElement?.Q<ComposeView>();
        composeView?.SetContent(ApplicationUtils.IsPlaying ? Content : Preview);
    }
}