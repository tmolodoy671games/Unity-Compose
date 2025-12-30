using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways]
public abstract partial class ComposeUI : MonoBehaviour
{
    private readonly ComposableContent ContentLambda;
    private readonly ComposableContent PreviewLambda;

    protected ComposeUI()
    {
        ContentLambda = Content;
        PreviewLambda = Preview;
    }

    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>().SetContent(ContentLambda);
    }

    [Composable]
    protected abstract void Content();

    [Composable]
    protected virtual void Preview()
    {
    }

    private void OnEnable()
    {
        if (ApplicationUtils.IsPlaying)
            return;
        GetComponent<UIDocument>()?.rootVisualElement?.Q<ComposeView>()?.SetContent(PreviewLambda);
    }

    [Button]
    protected void PrintTreeStructure()
    {
        Debug.Log(CurrentComposer);
        SimpleLogger.Log(CurrentComposer);
    }
}
