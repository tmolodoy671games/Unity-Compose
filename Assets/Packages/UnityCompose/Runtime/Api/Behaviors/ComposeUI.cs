using System;
using System.IO;
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
        Debug.Log(CurrentComposer.Format());
    }

    [Button]
    protected void PrintSlots()
    {
        Debug.Log(CurrentComposer.SlotsToString());
    }

    [Button]
    protected void PrintTreeStructureToFile()
    {
        using TextWriter writer = new StreamWriter("output.txt");
        CurrentComposer.WriteToFile(writer);
    }

    [Button]
    private void PrintSlotsToFile()
    {
        using TextWriter writer = new StreamWriter("output.txt");
        CurrentComposer.WriteSlotsToFile(writer);
    }

    private UIDocument GetUiDocument() => GetComponent<UIDocument>().NotNull();
}