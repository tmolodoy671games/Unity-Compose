using System;
using System.Diagnostics.CodeAnalysis;
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

    protected virtual SlotTableType SlotTableType => SlotTableType.Stable;

    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        _document ??= GetUiDocument();
        var composeView = _document.rootVisualElement.Q<ComposeView>();
        composeView.Type = SlotTableType;
        _document.rootVisualElement.Q<ComposeView>().SetContent(__Content);
    }

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    protected abstract void Content();

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    protected virtual void Preview()
    {
    }

    private void OnEnable()
    {
        _document ??= GetUiDocument();
        var composeView = _document?.rootVisualElement?.Q<ComposeView>();
        if (ApplicationUtils.IsPlaying)
        {
            if (composeView != null)
                composeView.Type = SlotTableType;
            composeView?.SetContent(__Content);
            return;
        }

        composeView?.SetContent(__Preview);
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