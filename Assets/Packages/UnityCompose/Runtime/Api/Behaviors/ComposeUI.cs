using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways]
public abstract partial class ComposeUI : MonoBehaviour
{
    private readonly Action ContentLambda;
    private readonly Action PreviewLambda;

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
    private void PrintTreeStructure()
    {
        var group = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>().userData.CastTo<IComposeGroupDeprecated>();
        Debug.Log(group.ToString(recursive: true));
    }
}
