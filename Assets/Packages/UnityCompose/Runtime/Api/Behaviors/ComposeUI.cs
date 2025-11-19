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
    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>().SetContent(ContentImpl);
    }

    [Composable]
    protected abstract void Content();

    [Composable]
    private void ContentImpl()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        Content();
    }

    [Composable]
    protected virtual void Preview()
    {
    }

    private void OnEnable()
    {
        GetComponent<UIDocument>()?.rootVisualElement?.Q<ComposeView>()?.SetContent(ContentImpl);
    }

    private void OnDisable()
    {
        GetComponent<UIDocument>()?.rootVisualElement?.Q<ComposeView>()?.SetContent(static () => {});
    }

    [Button]
    private void PrintTreeStructure()
    {
        var group = GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>().userData.CastTo<IComposeGroup>();
        Debug.Log(group.ToString(recursive: true));
    }
}

public abstract partial class ComposeUI
{
#if UNITY_EDITOR

    private void Update()
    {
        if (ApplicationUtils.IsPlaying) return;
        var document = GetComponent<UIDocument>();
        if (!document) return;
        var composeView = document.rootVisualElement?.Q<ComposeView>();
        composeView?.SetContent(Preview);
    }
#endif
}