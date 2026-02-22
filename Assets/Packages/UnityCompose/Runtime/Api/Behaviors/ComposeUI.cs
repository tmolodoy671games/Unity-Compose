using System;
using SharpExtensions;
using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways, HideMonoScript]
public abstract partial class ComposeUI : MonoBehaviour
{
    private UIDocument? _document;
    
    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        _document ??= GetUiDocument();
        _document.rootVisualElement.Q<ComposeView>().SetContent(__Content);
    }

    [Composable]
    protected abstract void Content();

    [Composable]
    protected virtual void Preview()
    {
    }

    private void OnEnable()
    {
        _document ??= GetUiDocument();
        if (ApplicationUtils.IsPlaying)
        {
            _document?.rootVisualElement?.Q<ComposeView>()?.SetContent(__Content);
            return;
        }

        _document?.rootVisualElement?.Q<ComposeView>()?.SetContent(__Preview);
    }

    [Button]
    protected void PrintTreeStructure()
    {
        Debug.Log(CurrentComposer);
    }

    [Button]
    protected void PrintSlots()
    {
        Debug.Log(CurrentComposer.SlotsToString());
    }

    private UIDocument GetUiDocument() => GetComponent<UIDocument>().NotNull();
}