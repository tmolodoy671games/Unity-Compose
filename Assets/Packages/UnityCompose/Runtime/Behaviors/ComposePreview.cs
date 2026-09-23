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

    private void OnEnable()
    {
        if (ApplicationUtils.IsPlaying) return;
        var document = GetComponent<UIDocument>();
        if (!document) return;
        var composeView = document.rootVisualElement?.Q<ComposeView>();
        composeView?.SetContent(Preview);
    }
}